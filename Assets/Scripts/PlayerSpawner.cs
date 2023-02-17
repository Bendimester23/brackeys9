using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject currentPlayer;
    public CinemachineVirtualCamera vCam;

    public Transform mapCenter;

    private bool _isZoomedOut;
    private bool _isZooming;

    public float zoomSpeed = 10.0f;

    private readonly List<GameObject> _oldPlayers = new List<GameObject>();

    [FormerlySerializedAs("text")] public TextMeshProUGUI livesText;
    public GameObject respawnPanel;

    private int _lives;
    public int maxLives = 1;

    public bool allowJumping;

    public bool allowZoomingOut = false;
    
    private void Start()
    {
        _lives = maxLives;
        _lives--;
        
        currentPlayer = Instantiate(playerPrefab, transform.position, transform.rotation);
        vCam.m_Follow = currentPlayer.transform;
        currentPlayer.GetComponent<PlayerMovement>().onDie.AddListener(SpawnNewPlayer);
        currentPlayer.GetComponent<PlayerMovement>().canJump = allowJumping;
        currentPlayer.tag = "Player";
        UpdateText();
    }

    private void UpdateText()
    {
        livesText.text = $"Robots left: {_lives}";
    }

    private void GameOver()
    {
        respawnPanel.SetActive(true);
        currentPlayer.GetComponent<PlayerMovement>().canControl = false;
    }

    public void Respawn()
    {
        _oldPlayers.ForEach(Destroy);
        _oldPlayers.Clear();
        Destroy(currentPlayer);
        respawnPanel.SetActive(false);
        CreatePlayer();
        _lives = maxLives - 1;
        UpdateText();
    }

    private void CreatePlayer()
    {
        currentPlayer = Instantiate(playerPrefab, transform.position, transform.rotation);
        vCam.m_Follow = currentPlayer.transform;
        var playerComponent = currentPlayer.GetComponent<PlayerMovement>();
        playerComponent.onDie.AddListener(SpawnNewPlayer);
        playerComponent.canJump = allowJumping;
        currentPlayer.tag = "Player";
    }

    private void SpawnNewPlayer()
    {
        if (_lives == 0)
        {
            Debug.Log("You ded");
            GameOver();
            return;
        }

        _lives--;
        var playerComponent = currentPlayer.GetComponent<PlayerMovement>();
        playerComponent.canControl = false;
        playerComponent.onDie.RemoveAllListeners();
        _oldPlayers.Add(currentPlayer);
        CreatePlayer();
        UpdateText();
    }

    private void Update()
    {
        if (_isZooming)
        {
            float target = _isZoomedOut ? 15 : 5;
            if (Mathf.Abs(vCam.m_Lens.OrthographicSize - target) < 0.05f) _isZooming = false;
            vCam.m_Lens.OrthographicSize = Mathf.Lerp(vCam.m_Lens.OrthographicSize, target, Time.deltaTime * zoomSpeed);
        }

        if (!allowZoomingOut || !Input.GetKeyDown(KeyCode.M)) return;
        vCam.m_Follow = _isZoomedOut ? currentPlayer.transform : mapCenter;

        _isZoomedOut = !_isZoomedOut;
        _isZooming = true;
    }
}

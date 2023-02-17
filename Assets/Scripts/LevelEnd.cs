using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    public int nextScene;

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Don't go to the next scene if a dead player enters the gate
        if (other.CompareTag("Player") && other.GetComponent<PlayerMovement>().canControl) LoadNextScene();
    }
}

using UnityEngine;
using UnityEngine.Events;

public class RedButton : MonoBehaviour
{
    public UnityEvent onPress;

    public UnityEvent onRelease;

    public GameObject redThing;
    public ButtonSensor redSensor;

    public Transform pressedPos;
    public Transform releasedPos;

    [Range(0, 20)]
    public float speed;

    public bool pressed;
    private bool _moving;
    
    private void Start()
    {
        redSensor = redThing.GetComponent<ButtonSensor>();
        
        redSensor.onPress.AddListener(OnBtnPressed);
        redSensor.onRelease.AddListener(OnBtnReleased);
    }

    private void OnBtnPressed()
    {
        _moving = true;
        pressed = true;
        
        onPress.Invoke();
    }
    
    private void OnBtnReleased()
    {
        _moving = true;
        pressed = false;
        
        onRelease.Invoke();
    }

    private void Update()
    {
        if (!_moving) return;
        var toPos = pressed ? pressedPos.localPosition : releasedPos.localPosition;

        redThing.transform.localPosition = Vector3.Lerp(redThing.transform.localPosition, toPos, Time.deltaTime * speed);
        if (Vector3.SqrMagnitude(redThing.transform.localPosition - toPos) < 0.001f) _moving = false;
    }
}

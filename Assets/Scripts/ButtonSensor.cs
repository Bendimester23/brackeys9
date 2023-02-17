using UnityEngine;
using UnityEngine.Events;

public class ButtonSensor : MonoBehaviour
{
    public UnityEvent onPress;
    public UnityEvent onRelease;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) onPress.Invoke();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) onRelease.Invoke();
    }
}

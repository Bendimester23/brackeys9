using UnityEngine;
using UnityEngine.Events;

public class SpringUpgrade : MonoBehaviour
{
    private float _rotation;

    public float rotationSpeed = 30.0f;

    public UnityEvent onPickedUp;

    void Update()
    {
        _rotation = (_rotation + rotationSpeed * Time.deltaTime) % 360.0f; 
        transform.rotation = Quaternion.Euler(0, 0, _rotation);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        onPickedUp.Invoke();
        other.GetComponent<PlayerMovement>().canJump = true;
        other.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 6000.0f));
        Destroy(gameObject);
    }
}

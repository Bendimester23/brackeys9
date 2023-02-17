using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    private bool _open;

    private bool _moving;

    public Vector3 closedPos;
    public Vector3 openPos;

    [Range(0, 20)]
    public float speed = 10.0f;

    private void Update()
    {
        if (!_moving) return;
        var toPos = _open ? openPos : closedPos;
        transform.position = Vector3.Lerp(transform.position, toPos, Time.deltaTime * speed);
        if (Vector3.SqrMagnitude(transform.position - toPos) < 0.01f) _moving = false;
    }

    public void Open()
    {
        _open = true;
        _moving = true;
    }
    
    public void Close()
    {
        _open = false;
        _moving = true;
    }
}

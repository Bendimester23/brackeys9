using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    private float _input;

    [Range(0, 100)]
    public float speed = 10;

    [Range(0, 50)]
    public float inputSnappyness = 10;

    [Range(0, 1000)]
    public float jumpPower = 10;

    [Range(0, 1)]
    public float airControl = 1.0f;

    private bool _jump;

    private bool _onGround;

    public Transform groundCheckPos;
    [Range(0, 2)] public float groundCheckRadius = 0.1f;

    [Header("Capabilities")] public bool canJump = true;
    public bool canControl = true;
    public bool canDie = true;

    public UnityEvent onDie;

    public Collider2D aliveCollider;
    public Collider2D deadCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        aliveCollider.enabled = true;
        deadCollider.enabled = false;

        onDie ??= new UnityEvent();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canControl) return;
        _input = Mathf.Lerp(_input, Input.GetAxis("Horizontal") * 10.0f, Time.deltaTime * inputSnappyness);
        if (canJump && _onGround && Input.GetKeyDown(KeyCode.Space)) _jump = true;
        
        if (!canDie || !Input.GetKeyDown(KeyCode.E)) return;
        aliveCollider.enabled = false;
        deadCollider.enabled = true;
        onDie?.Invoke();
    }

    private void FixedUpdate()
    {
        if (!canControl) return;
        var col = Physics2D.OverlapCircle(groundCheckPos.position, groundCheckRadius);
        _onGround = col && col.CompareTag("Ground");
        if (_jump)
        {
            _jump = false;
            _rigidbody.AddForce(new Vector2(0, jumpPower * 1000.0f));
        }

        _rigidbody.velocity = new Vector2(_input * Time.fixedDeltaTime * speed * (_onGround ? 1 : airControl), _rigidbody.velocity.y);
    }
}

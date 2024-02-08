using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpAmount;

    private InputHandler inputHandler;
    private Rigidbody2D playerRb;
    private Vector3 scale;
    private bool canJump;

    public static PlayerController Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        inputHandler = FindObjectOfType<InputHandler>();
        playerRb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        Vector2 inputVector = inputHandler.GetMovementVector();
        Vector3 movementVector = new Vector3(inputVector.x, 0f, 0f);
        transform.Translate(movementVector * movementSpeed * Time.deltaTime);

        float sign = movementVector.x > 0 ? 1 : movementVector.x < 0 ? -1 : transform.localScale.x;
        scale = new Vector3(sign * 1, transform.localScale.y,1f);
        transform.localScale = scale;
    }

    public void Jump()
    {
        if (canJump)
        {
            playerRb.AddForce(Vector2.up * jumpAmount,ForceMode2D.Impulse);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            canJump = false;
        }
    }
}

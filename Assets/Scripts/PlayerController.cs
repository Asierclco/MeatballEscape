using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Vector3 respawnPoint;
    public float fallLimit;

    private Rigidbody rb;
    private float jumpForce;
    private bool isOnGround;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        respawnPoint = transform.position;
    }

    void Update()
    {
        if (transform.position.y < fallLimit)
        {
            rb.linearVelocity = Vector3.zero;
            transform.position = respawnPoint;
        }

        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isOnGround)
            {
                jumpForce = 65;
            }
        }
        else
        {
            jumpForce = 0;
        }

        Vector3 movement = new Vector3(inputHorizontal, jumpForce, inputVertical);
        rb.AddForce(movement * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        isOnGround = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isOnGround = false;
    }
}
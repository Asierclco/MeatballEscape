using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private bool jump = false;

    public float speed;
    public float jumpForce;
    public Vector3 respawnPoint;
    public float fallLimit;

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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(inputHorizontal, 0, inputVertical);
        rb.AddForce(movement * speed);

        if (jump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jump = false;
        }

    }

/*
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUpAble"))
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("PowerUp"))
        {
            Debug.Log("Has recogido un power up!");
            Destroy(other.gameObject);
        }
    }
*/
}

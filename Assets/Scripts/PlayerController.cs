using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Vector3 respawnPoint;
    public float fallLimit;
    public float maxSpeed;
    private float startMaxSpeed;
    public float powerUpScale;
    public Transform camara;
    private Rigidbody rb;
    private float jumpForce;
    private bool isOnGround;
    public float speedPowerUpTime;
    public AudioSource hitSound;

    private bool hasJumpBoost = false;

    void Start()
    {
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
        respawnPoint = transform.position;
        startMaxSpeed = maxSpeed;
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

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
            if (hasJumpBoost)
                jumpForce = 65*4;
            else
                jumpForce = 65;
        else
            jumpForce = 0;

        // Usar forward de la cámara
        Vector3 forward = Vector3.ProjectOnPlane(camara.forward, Vector3.up).normalized;
        Vector3 right = Vector3.ProjectOnPlane(camara.right, Vector3.up).normalized;

        Vector3 movement = right * inputHorizontal + forward * inputVertical;

        movement = Vector3.ClampMagnitude(movement, 1f);

        Vector3 finalMovement = new Vector3(movement.x, jumpForce, movement.z);

        rb.AddForce(finalMovement * speed);
    }

    private void FixedUpdate() { LimitVelocity(); }

    private void OnCollisionEnter(Collision collision)
    {
        hitSound.Play();
        CheckGround(collision);

        if (collision.gameObject.CompareTag("Pickupable"))
        {
            collision.gameObject.transform.SetParent(transform, true);
            collision.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        // Ketchup
        if (collision.gameObject.CompareTag("SpeedBoost"))
        {
            
            collision.gameObject.transform.SetParent(transform, true);
            collision.gameObject.GetComponent<BoxCollider>().enabled = false;
            maxSpeed *= 2.0f;

            Invoke("ResetSpeed", speedPowerUpTime);
            Destroy(collision.gameObject, speedPowerUpTime);
        }
        // Muelle
        if (collision.gameObject.CompareTag("JumpBoost"))
        {
            hasJumpBoost = true;
            collision.gameObject.transform.SetParent(transform, true);
            collision.gameObject.GetComponent<BoxCollider>().enabled = false;
            maxSpeed *= 2.0f;

            Invoke("ResetJump", speedPowerUpTime);
            Destroy(collision.gameObject, speedPowerUpTime);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            transform.localScale = new Vector3(transform.localScale.x / powerUpScale,
                                               transform.localScale.y / powerUpScale,
                                               transform.localScale.z / powerUpScale);

            Destroy(collision.gameObject);
        }
    }

    private void ResetSpeed() { maxSpeed = startMaxSpeed; }
    private void ResetJump() { hasJumpBoost = false; }

    private void OnCollisionStay(Collision collision) { CheckGround(collision); }

    private void OnCollisionExit(Collision collision) { isOnGround = false; }

    private void CheckGround(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (Vector3.Angle(contact.normal, Vector3.up) < 60f)
            {
                isOnGround = true;
                return;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUP"))
        {
            Destroy(other.gameObject);

            transform.localScale = new Vector3(transform.localScale.x * powerUpScale,
                                               transform.localScale.y * powerUpScale,
                                               transform.localScale.z * powerUpScale);
        }
    }

    private void LimitVelocity()
    {
        Vector3 horizontal = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        horizontal = Vector3.ClampMagnitude(horizontal, maxSpeed);
        rb.linearVelocity = new Vector3(horizontal.x, rb.linearVelocity.y, horizontal.z);
    }
}
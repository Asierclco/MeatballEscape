using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    [Header("=== PLAYER / MOVIMIENTO ===")]
    public float speed;
    public Vector3 respawnPoint;
    public float fallLimit;
    public float maxSpeed;
    public float powerUpScale;
    public Transform camara;
    public float speedPowerUpTime;
    public float jumpPowerUpForce;
    public uint vidas;

    [Header("=== AUDIO ===")]
    public AudioSource hitSound;
    public AudioSource hurtSound;
    public AudioSource speedPowerUpSound;
    public AudioSource jumpPowerUpSound;
    public AudioSource multiplierPowerUpSound;
    public AudioSource deathSound;

    // Globales privadas
    private bool hasJumpBoost = false;
    private float jumpForce;
    private Rigidbody rb;
    private bool isOnGround;
    private float startMaxSpeed;

    void Start()
    {
        vidas = 3;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
        respawnPoint = transform.position;
        startMaxSpeed = maxSpeed;
    }

    void Update()
    {
        if (transform.position.y < fallLimit)
        {
            deathSound.Play();
            rb.linearVelocity = Vector3.zero;
            transform.position = respawnPoint;
        }

        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
            if (hasJumpBoost)
                jumpForce = 65 * jumpPowerUpForce;
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
            speedPowerUpSound.Play();
            collision.gameObject.transform.SetParent(transform, true);
            collision.gameObject.GetComponent<BoxCollider>().enabled = false;
            maxSpeed *= 2.0f;

            Invoke("ResetSpeed", speedPowerUpTime);
            Destroy(collision.gameObject, speedPowerUpTime);
        }
        // Muelle
        if (collision.gameObject.CompareTag("JumpBoost"))
        {
            jumpPowerUpSound.Play();
            hasJumpBoost = true;
            collision.gameObject.transform.SetParent(transform, true);
            collision.gameObject.GetComponent<BoxCollider>().enabled = false;
            maxSpeed *= 2.0f;

            Invoke("ResetJump", speedPowerUpTime);
            Destroy(collision.gameObject, speedPowerUpTime);
        }
        // Enemigo
        if (collision.gameObject.CompareTag("Enemy"))
        {
            vidas--;
            transform.localScale = new Vector3(transform.localScale.x * 0.75f,
                                               transform.localScale.y * 0.75f,
                                               transform.localScale.z * 0.75f);
            if (vidas != 0) hurtSound.Play();
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
        // PowerUPs
        if (other.CompareTag("PowerUP"))
        {
            vidas++;
            speedPowerUpSound.Play();
            Destroy(other.gameObject);

            transform.localScale = new Vector3(transform.localScale.x * powerUpScale,
                                               transform.localScale.y * powerUpScale,
                                               transform.localScale.z * powerUpScale);
        }

        // Multiplicador
        if (other.CompareTag("PointMultiplier"))
        {
            if (CoinManager.Instance != null)
                CoinManager.Instance.MultiplyPoints();
            
            multiplierPowerUpSound.Play();
            Destroy(other.gameObject);
        }
    }

    private void LimitVelocity()
    {
        Vector3 horizontal = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        horizontal = Vector3.ClampMagnitude(horizontal, maxSpeed);
        rb.linearVelocity = new Vector3(horizontal.x, rb.linearVelocity.y, horizontal.z);
    }
}
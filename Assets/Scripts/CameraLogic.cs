using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CameraLogic : MonoBehaviour
{
    [Header("Movimiento")]
    public float fuerza = 20f;
    public float velocidadMaxima = 10f;
    public float velocidadGiro = 120f;

    [Header("Cámara")]
    public Transform camara;
    public Vector3 distancia = new Vector3(0, 5, -8);
    public float suavizado = 8f;

    private Rigidbody rb;
    private float avance;
    private float giro;
    private float angulo;
    private Quaternion orientacionCamara;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        angulo = transform.eulerAngles.y;
        orientacionCamara = Quaternion.Euler(0, angulo, 0);
    }

    void Update()
    {
        avance = Input.GetAxis("Vertical");
        giro = Input.GetAxis("Horizontal");

        // Guarda una orientación independiente del rodamiento.
        angulo += giro * velocidadGiro * Time.deltaTime;
    }

    void FixedUpdate()
    {
        Quaternion orientacion = Quaternion.Euler(0, angulo, 0);
        Vector3 direccion = orientacion * Vector3.forward;

        rb.AddForce(direccion * avance * fuerza);

        // Limita X/Z sin modificar la velocidad vertical.
        Vector3 velocidad = rb.linearVelocity;

        Vector3 horizontal = new Vector3(velocidad.x, 0, velocidad.z);
        horizontal = Vector3.ClampMagnitude(horizontal, velocidadMaxima);
        rb.linearVelocity = new Vector3(horizontal.x, velocidad.y, horizontal.z);
    }

    void LateUpdate()
    {
        if (camara == null) return;

        Quaternion orientacionObjetivo = Quaternion.Euler(0, angulo, 0);

        float factor = 1f - Mathf.Exp(-suavizado * Time.deltaTime);

        orientacionCamara = Quaternion.Slerp(orientacionCamara, orientacionObjetivo, factor);
        camara.position = transform.position + orientacionCamara * distancia;
        camara.LookAt(transform.position, Vector3.up);
    }
}
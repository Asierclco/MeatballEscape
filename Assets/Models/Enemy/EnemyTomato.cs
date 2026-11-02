using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyTomato : MonoBehaviour
{
    [SerializeField] private Transform jugador;
    [SerializeField] private float velocidad = 4f;
    public float velocidadInicial;
    [SerializeField] private float distanciaParada = 1.5f;

    public ActivateTriggerEnemy ate;
    private bool hasEntered;

    private Rigidbody rb;


    private void Awake()
    {
        velocidadInicial = velocidad;
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        hasEntered = ate.hasEntered;
        Debug.Log("Has entered:" + hasEntered);
    }

    private void LateUpdate()
    {
        hasEntered = ate.hasEntered;
    }

    private void FixedUpdate()
    {
        if (jugador == null) return;
        Debug.Log("Has entered:" + hasEntered);
        
        if (hasEntered)
        {
            velocidad = velocidadInicial;
            Vector3 direccion = jugador.position - rb.position;
            direccion.y = 0f;

            if (direccion.magnitude <= distanciaParada)
            {
                rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
                return;
            }

            direccion.Normalize();

            rb.linearVelocity = new Vector3(direccion.x * velocidad,
                                            rb.linearVelocity.y,
                                            direccion.z * velocidad);
        }
        else
        {
            Debug.Log("Esta fuera");
            velocidad = 0.0f;
        }
    }
}

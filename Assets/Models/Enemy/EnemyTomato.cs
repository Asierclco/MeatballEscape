using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyTomato : MonoBehaviour
{
    [SerializeField] private Transform jugador;
    [SerializeField] private float velocidad = 4f;
    [SerializeField] private float distanciaParada = 1.5f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (jugador == null) return;

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
}

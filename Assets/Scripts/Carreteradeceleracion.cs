
using UnityEngine;

public class Carreteradeceleracion : MonoBehaviour
{
    public float deceleracion = 5f;

    private void OnCollisionStay(Collision collision)
    {
        Rigidbody rb = collision.rigidbody;

        if (rb != null)
        {
            Vector3 velocidad = new Vector3(
                rb.linearVelocity.x,
                0,
                rb.linearVelocity.z
            );

            if (velocidad.magnitude > 0.1f)
            {
                Vector3 fuerza = -velocidad.normalized * deceleracion;

                rb.AddForce(fuerza, ForceMode.Acceleration);
            }
        }
    }
}

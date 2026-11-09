

using UnityEngine;

public class Carreteraaceleracion : MonoBehaviour
{
    public float aceleracion = 5f;

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
                rb.AddForce(
                    velocidad.normalized * aceleracion,
                    ForceMode.Acceleration
                );
            }
        }
    }
}



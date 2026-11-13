
using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    public Transform camara;
    public Vector3 distancia = new Vector3(0, 5, -8);
    public float sensibilidad = 3f;
    public float suavizado = 8f;

    private float angulo;
    private Quaternion orientacionCamara;

    void Awake()
    {
        angulo = transform.eulerAngles.y;
        orientacionCamara = Quaternion.Euler(0, angulo, 0);
    }

    void Update()
    {
        float giroMouse = Input.GetAxis("Mouse X");
        angulo += giroMouse * sensibilidad;
    }

    void LateUpdate()
    {
        if (camara == null) return;

        Quaternion orientacionObjetivo =
        Quaternion.Euler(0, angulo, 0);

        float factor = 1f - Mathf.Exp(-suavizado * Time.deltaTime);

        orientacionCamara = Quaternion.Slerp(orientacionCamara, orientacionObjetivo, factor);
        camara.position = transform.position + orientacionCamara * distancia;

        camara.LookAt(transform.position, Vector3.up);
    }
}
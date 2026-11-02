using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemigosActividades : MonoBehaviour
{
    public float velocidad;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 4)
        {
            velocidad = -velocidad;
        }
        if (transform.position.x < -5)
        {
            velocidad = -velocidad;
        }
        transform.Translate(velocidad + Time.deltaTime, 0, 0);

    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("me he chocado con un enemigo");

    }

}

using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingEnemy : MonoBehaviour
{
    public float velocity;

    void Start()
    {
        Color randomColor = new Color(Random.value, Random.value, Random.value, 1.0f);
        GetComponent<Renderer>().material.color = randomColor;
    }

    void Update()
    {
        if (transform.position.x > 6)
        {
            Color newColor = new Color(Random.value, Random.value, Random.value, 1.0f);
            GetComponent<Renderer>().material.color = newColor;
            velocity = -velocity;
        }
        else if (transform.position.x < -8)
        {
            Color newColor2 = new Color(Random.value, Random.value, Random.value, 1.0f);
            GetComponent<Renderer>().material.color = newColor2;
            velocity = -velocity;
        }

        transform.Translate(velocity*Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene("Level1");
    }
}

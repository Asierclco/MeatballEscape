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
        float x = transform.position.x;

        if (x > 4f && velocity > 0f)
        {
            velocity = -Mathf.Abs(velocity);
            SetRandomColor();
            x = 4f;
        }
        else if (x < -4f && velocity < 0f)
        {
            velocity = Mathf.Abs(velocity);
            SetRandomColor();
            x = -4f;
        }

        transform.position = new Vector3(
            x + velocity * Time.deltaTime,
            transform.position.y,
            transform.position.z);
    }

    private void SetRandomColor()
    {
        GetComponent<Renderer>().material.color =
            new Color(Random.value, Random.value, Random.value, 1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            SceneManager.LoadScene("Level1");
    }
}

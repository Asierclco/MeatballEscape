using UnityEngine;
using UnityEngine.SceneManagement;

public class StaticEnemy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene("Level1");
    }
}

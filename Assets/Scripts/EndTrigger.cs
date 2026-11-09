using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour
{
    public string NombreDelMapa;
    private void OnTriggerEnter(Collider other) { SceneManager.LoadScene(NombreDelMapa); }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class ActivateTriggerEnemy : MonoBehaviour
{
    public bool hasEntered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            hasEntered = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            hasEntered = false;
    }
}

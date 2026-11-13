using UnityEngine;
using TMPro;

public class CoinLogic : MonoBehaviour
{
    public AudioSource collectSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (CoinManager.Instance != null) CoinManager.Instance.AddPoints(1);
            collectSound.Play();
            Destroy(gameObject);
        }
    }
}
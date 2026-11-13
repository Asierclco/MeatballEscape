using Unity.VectorGraphics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class LifeManager : MonoBehaviour
{
    public GameObject player;
    public GameObject VIDA1, VIDA2, VIDA3;
    public AudioSource sonidoMuerte;

    void FixedUpdate()
    {
        if (player.GetComponent<PlayerController>().vidas == 3)
        {
            VIDA1.SetActive(true);
            VIDA2.SetActive(true);
            VIDA3.SetActive(true);
        }
        else if (player.GetComponent<PlayerController>().vidas == 2)
        {
            VIDA1.SetActive(true);
            VIDA2.SetActive(true);
            VIDA3.SetActive(false);
        }
        else if (player.GetComponent<PlayerController>().vidas == 1)
        {
            VIDA1.SetActive(true);
            VIDA2.SetActive(false);
            VIDA3.SetActive(false);
        }
        else if (player.GetComponent<PlayerController>().vidas == 0)
        {
            VIDA1.SetActive(false);
            VIDA2.SetActive(false);
            VIDA3.SetActive(false);

            sonidoMuerte.Play();
            RestartPlayer();

            VIDA1.SetActive(true);
            VIDA2.SetActive(true);
            VIDA3.SetActive(true);

            player.GetComponent<PlayerController>().vidas = 3;
        }

        if (player.GetComponent<PlayerController>().vidas > 3)
            player.GetComponent<PlayerController>().vidas = 3;
    }

    private void RestartPlayer()
    {
        player.GetComponent<PlayerController>().transform.localScale = new Vector3(24.87f, 24.87f, 24.87f);
        player.GetComponent<PlayerController>().transform.position = new Vector3(player.GetComponent<PlayerController>().respawnPoint.x,
                                                                                 player.GetComponent<PlayerController>().respawnPoint.y,
                                                                                 player.GetComponent<PlayerController>().respawnPoint.z);
    }
}

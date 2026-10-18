using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject cfgMenu;
    private bool isCfgActive = false;

    private void Start()
    {
        cfgMenu.SetActive(isCfgActive);
    }

    public void Empezar()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Salir()
    {
        Debug.Log("Saliendo...");
        Application.Quit();
    }

    public void MostrarOpciones()
    {
        isCfgActive = !isCfgActive;
        cfgMenu.SetActive(isCfgActive);
    }
}
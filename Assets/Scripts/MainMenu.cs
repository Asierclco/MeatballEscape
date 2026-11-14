using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject credMenu;
    private bool isCredActive = false;

    private void Start()
    {
        Cursor.visible = true;
        credMenu.SetActive(isCredActive);
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

    public void MostrarCreditos()
    {
        isCredActive = !isCredActive;
        credMenu.SetActive(isCredActive);
    }
}
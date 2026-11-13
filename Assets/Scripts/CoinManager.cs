using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; }

    public TMP_Text textUI;
    private uint puntosTotales = 0;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else Instance = this;
    }

    public void AddPoints(uint amount)
    {
        puntosTotales += amount;
        UpdateUI();
    }

    public void MultiplyPoints()
    {
        puntosTotales *= 2;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (textUI != null)
            textUI.text = puntosTotales.ToString();
    }
}
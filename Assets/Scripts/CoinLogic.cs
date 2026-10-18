using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CoinLogic : MonoBehaviour
{
    public TMP_Text textUI;
    private uint puntos = 0;

    private void OnTriggerEnter(Collider other)
    {
        puntos = Convert.ToUInt32(textUI.text);
        puntos++;

        textUI.text = puntos.ToString();
        
        Destroy(gameObject);
    }
}

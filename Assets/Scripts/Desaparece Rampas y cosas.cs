using UnityEngine;

public class DesapareceRampayCosas : MonoBehaviour
{
    public GameObject RAMPA;
    public GameObject RAMPA2;
    public GameObject MURO;
    public GameObject RECTA;
    public GameObject RECTA2;
    public GameObject RECTA3;
    public GameObject CURVA;
    public GameObject CURVA2;
    public GameObject CURVA3;
    public GameObject CURVA4;







    private void OnTriggerEnter(Collider other)
    {
        RAMPA.SetActive(false);
        RAMPA2.SetActive(false);
        MURO.SetActive(false);
        RECTA.SetActive(false);
        RECTA2.SetActive(false);
        RECTA3.SetActive(false);
        CURVA.SetActive(false);
        CURVA2.SetActive(false);
        CURVA3.SetActive(false);
        CURVA4.SetActive(false);

    }
}

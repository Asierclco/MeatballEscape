using UnityEngine;

public class APARECERAMPA : MonoBehaviour
{
    public GameObject RAMPA;
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
        RAMPA.SetActive(true);
        MURO.SetActive(true);
        RECTA.SetActive(true);
        RECTA2.SetActive(true);
        RECTA3.SetActive(true);
        CURVA.SetActive(true);
        CURVA2.SetActive(true);
        CURVA3.SetActive(true);
        CURVA4.SetActive(true);

    }
}

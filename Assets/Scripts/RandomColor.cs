using UnityEngine;

public class RandomColor : MonoBehaviour
{
    void Start()
    {
        Color randomColor = new Color(Random.value, Random.value, Random.value, 1.0f);
        GetComponent<Renderer>().material.color = randomColor;
    }
}
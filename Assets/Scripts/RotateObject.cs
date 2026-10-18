using Unity.Mathematics;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float velocity;
    public bool RotateX;
    public bool RotateY;
    public bool RotateZ;
    void Update()
    {
        float vel_x, vel_y, vel_z;
        if (RotateX)
        {
            vel_x = velocity;
        }
        else { vel_x = 0.0f; }
        if (RotateY)
        {
            vel_y = velocity;
        } else { vel_y = 0.0f; }
        if (RotateZ)
        {
            vel_z = velocity;
        } else { vel_z = 0.0f; }

        transform.Rotate(vel_x * Time.deltaTime, vel_y * Time.deltaTime, vel_z * Time.deltaTime);
    }
}

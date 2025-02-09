using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Gracz do œledzenia
    public Vector3 offset = new Vector3(0, 5, -10); // Offset kamery wzglêdem gracza
    public float smoothSpeed = 0.125f; // Szybkoœæ œledzenia

    private void LateUpdate()
    {
        if (target != null)
        {
            // Oblicz now¹ pozycjê kamery
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Ustaw now¹ pozycjê kamery
            transform.position = smoothedPosition;
        }
    }
}
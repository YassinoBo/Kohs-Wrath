using UnityEngine;

public class ScrollingEnvironment : MonoBehaviour
{
    public float scrollSpeed = 10.0f; // Geschwindigkeit der Umgebung
    public float resetPosition = -50.0f; // Position, bei der das Objekt zurückgesetzt wird
    public float startPosition = 50.0f; // Startposition der Umgebung

    void Update()
    {
        // Bewege das Objekt entlang der Z-Achse
        transform.Translate(Vector3.back * scrollSpeed * Time.deltaTime);

        // Reset, wenn das Objekt zu weit zurückgeht
        if (transform.position.z < resetPosition)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, startPosition);
        }
    }
}

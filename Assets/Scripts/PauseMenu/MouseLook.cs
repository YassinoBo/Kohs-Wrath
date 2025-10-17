using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform playerBody;
    private float _sensitivity = 100f;  // Verwende ein privates Feld

    public float sensitivity  // Öffentliche Eigenschaft zur Steuerung des Zugriffs
    {
        get { return _sensitivity; }
        set { _sensitivity = value; }
    }

    public void SetSensitivity(float sensitivity)
    {
        this.sensitivity = sensitivity;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (sensitivity > 0)
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

            // Implementiere die vertikale Rotation
            float xRotation = -mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            // Wende die Rotation auf die Kamera an
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            // Drehung um die Y-Achse auf das Spielerobjekt anwenden
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }

}

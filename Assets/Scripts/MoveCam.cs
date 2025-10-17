using UnityEngine;

public class MoveCam : MonoBehaviour
{
    public bool canMoveCam = true;

    public float sens = 2f;
    public Transform player;
    private float xRotation = 0f;

    public static MoveCam Instance {get; private set;}
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        xRotation = transform.localRotation.eulerAngles.x;
    }

    private void Update()
    {
        if (canMoveCam)
        {
            float mouseX = Input.GetAxis("Mouse X") * sens * Time.smoothDeltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * sens * Time.smoothDeltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);


            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            player.Rotate(Vector3.up, mouseX);
        }
        else
        {
            return;
        }
    }
    public void LockCamera()
    {
        canMoveCam = false;
    }
}

using UnityEngine;

public class Interactable: MonoBehaviour, IInteractable
{
    public GameObject noteImage;
    public Material highlightMaterial;
    public Texture originalTexture;
    private Renderer objectRenderer;

    private MovePlayer movePlayer;

    private Camera playerCamera;
    private MoveCam moveCam;

    private SoundScript sound;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            objectRenderer.material.mainTexture = originalTexture;
        }

        moveCam = MoveCam.Instance;
        movePlayer = MovePlayer.Instance;
        
        sound = SoundScript.Instance;
    }

    public void Interact()
    {
        Debug.Log("open");
        
        sound.PaperSound(gameObject);

        if (moveCam != null)
        {
            moveCam.canMoveCam = false;
        }

        if (movePlayer != null)
        {
            movePlayer.canMove = false;
        }

        if (noteImage != null)
        {
            noteImage.SetActive(true);
        }
    }

    public void StopInteraction()
    {
        Debug.Log("close");


        if (moveCam != null)
        {
            moveCam.canMoveCam = true;
        }

        if (movePlayer != null)
        {
            movePlayer.canMove = true;
        }

        if (noteImage != null)
        {
            noteImage.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            objectRenderer.material = highlightMaterial;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            objectRenderer.material.mainTexture = originalTexture;
        }
    }
}

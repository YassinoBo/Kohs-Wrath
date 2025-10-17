using UnityEngine;

public class RedHuman : MonoBehaviour
{
    private bool isVisible = true;
    private SoundScript soundScript;
    public static RedHuman Instance {get; private set;}

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
    void Start()
    {
        soundScript = SoundScript.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isVisible)
        {
            gameObject.SetActive(false);
            soundScript.StairsSound();
        }
    }

    public void setVisible(bool visible)
    {
        this.isVisible = visible;
    }

    public bool getVisible()
    {
        return this.isVisible;
    }
}

using UnityEngine;

public class LampScript : MonoBehaviour
{
    public Light light;
    public Material lightMaterial;
    public Material lightOffMaterial;

    private bool isOn = true;
    public float fogLampOn = 5.25f;
    public float fogLampOff = 2.0f;

    public Renderer glassRenderer; // Renderer des Glases (f�r Materialwechsel)

    public static LampScript Instance { get; private set;}
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Verhindert doppelte Instanzen
        }
    }
    
    void Start()
    {
        // Ruft `ToggleLight` alle 10 Sekunden wiederholt auf
        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.Linear;
        RenderSettings.fogStartDistance = -fogLampOn;
        RenderSettings.fogEndDistance = fogLampOn;

        UpdateLampMaterial();

    }

    // Methode, die alle 10 Sekunden aufgerufen wird
    public void ToggleLight()
    {
        setStatus(false);
        Debug.Log("Lamp status toggled. New status: " + (isOn ? "On" : "Off"));
    }

    public bool getStatus()
    {
        return isOn;
    }

    public void setStatus(bool status)
    {
        isOn = status;
        if (light != null)
        {
            light.enabled = isOn; 
        }
        if (!isOn)
        {
            RenderSettings.fogEndDistance = fogLampOff;

        } else
        {
            RenderSettings.fogEndDistance = fogLampOn;
        }

        UpdateLampMaterial();

    }

    private void UpdateLampMaterial()
    {
        if (glassRenderer != null)
        {
            glassRenderer.material = isOn ? lightMaterial : lightOffMaterial;
        }
        else
        {
            Debug.LogWarning("Glass Renderer is not assigned!");
        }
    }

}

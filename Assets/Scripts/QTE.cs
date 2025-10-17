using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QTE : MonoBehaviour
{
    public Transform pointA, pointB;
    public RectTransform WinningZone;
    public float speed = 100f;
    public bool blockEvent = false;

    private float direction = 1f;
    private RectTransform pointer;
    private Vector3 target;
    

    private bool activeEvent = false;
    private int counter = 0;
    
    public static QTE Instance {get; private set;}

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
        pointer = GetComponent<RectTransform>();
        target = pointB.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSiblingVisibility(LampScript.Instance.getStatus());
        QTEHandler();
    }

    void QTEHandler()
    {
        if (!LampScript.Instance.getStatus() && !blockEvent)
        {
            pointer.anchoredPosition = Vector3.MoveTowards(pointer.anchoredPosition, target, speed * Time.deltaTime);

            if (Vector3.Distance(pointer.anchoredPosition, pointA.localPosition) < 0.1f)
            {
                target = pointB.localPosition;
                direction = 1f;
            }
            else if (Vector3.Distance(pointer.anchoredPosition, pointB.localPosition) < 0.1f)
            {
                target = pointA.localPosition;
                direction = -1f;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                CheckSuccess();
            }
        }
    }

    void CheckSuccess()
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(WinningZone, pointer.position, null))
        {
            Debug.Log("Winning Zone");
            LampScript.Instance.setStatus(true);
        }
        else
        {
            Debug.Log("Not Winning Zone");
        }
    }

    void UpdateSiblingVisibility(bool lampStatus)
    {
        if (!blockEvent)
        {
            // Hole die Sibling-Objekte
            Transform parent = transform.parent;
            if (parent == null) return;

            foreach (Transform sibling in parent)
            {
                // Image des Sibling-Objekts an/aus schalten
                UpdateImageVisibility(sibling, lampStatus);
                UpdateTextVisibility(sibling, lampStatus);

                if (sibling.name == "GreenArea")
                {
                    UpdateGreenAreaTransparency(sibling.GetComponent<Image>(), lampStatus);
                }

                // Durchlaufe auch die Kinder der Geschwister-Objekte
                foreach (Transform child in sibling)
                {
                    UpdateImageVisibility(child, lampStatus);
                    UpdateTextVisibility(child, lampStatus);

                }
            }
        }
    }

    // Helferfunktion, um die Sichtbarkeit eines Images basierend auf lampStatus zu setzen
    void UpdateImageVisibility(Transform obj, bool lampStatus)
    {
        Image image = obj.GetComponent<Image>();
        if (image != null)
        {
            image.enabled = !lampStatus;
        }
    }
    void UpdateTextVisibility(Transform obj, bool lampStatus)
    {
        TextMeshProUGUI text = obj.GetComponent<TextMeshProUGUI>();
        if (text != null)
        {
            text.enabled = !lampStatus;
        }
    }

    void UpdateGreenAreaTransparency(Image greenAreaImage, bool lampStatus)
    {
        if (greenAreaImage != null)
        {
            Color color = greenAreaImage.color;
            color.a = 0.0f; // Transparent (0.5) oder vollst�ndig sichtbar (1.0)
            greenAreaImage.color = color;
        }
    }
}

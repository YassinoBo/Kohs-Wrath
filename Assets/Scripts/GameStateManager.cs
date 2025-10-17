using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }
    public bool IsRestarted { get; set; } = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Objekt bleibt beim Szenenwechsel bestehen
        }
        else
        {
            Destroy(gameObject); // Verhindert Duplikate
        }
    }
}
using System;
using UnityEngine;

public class PushDown : MonoBehaviour
{
    public float pushForce = 5.0f;
    private RedGhostScript redGhostScript;
    private HeadTrigger headTrigger;
    public GameObject trigger;
    public GameObject headInLocker;

    private bool OneUse = true;

    private void Start()
    {
        redGhostScript = RedGhostScript.Instance;
        if (trigger == null && GameObject.FindGameObjectWithTag("HeadTrigger") != null)
        {
            trigger = GameObject.FindGameObjectWithTag("HeadTrigger");
        }
        headTrigger = trigger.GetComponent<HeadTrigger>();
        gameObject.SetActive(false);
    }

    void Update()
    {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null) 
            {
                Vector3 force = new Vector3(0, 0, -pushForce);
                rb.AddForce(force, ForceMode.Force);
            }
        
    }

    public void activate()
    {
        if (redGhostScript.HeadEvent && headTrigger.playerInZone && OneUse)
        {
            OneUse = false;
            gameObject.SetActive(true);
            headTrigger.deactivate();
        }
        headInLocker.SetActive(false);
    }
    
}
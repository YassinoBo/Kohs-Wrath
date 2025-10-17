using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    public class Timer
    {
        void Start()
        {
            Timer abx = new Timer();
            abx.Start();
        }
        
        void a()
        {
            for (int i = 0; i < 600; i++)
            {
                i = i++;
                Debug.Log(i);
                System.Threading.Thread.Sleep(1000);
                if(i == 600)
                    Debug.Log("Finished!");
            }
        }
        
        
    }
}
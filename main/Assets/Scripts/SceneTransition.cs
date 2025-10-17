using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour {
    
    private float length = 2.0f;
    public Image image;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void FadeBlack(System.Action next=null)
    {
        StartCoroutine(Fade(0f, 1f, next));
    }

    public void StayBlack(float duration, System.Action next = null)
    {
        StartCoroutine(StayBlackCoroutine(duration, next));
    }
    
    public void FadeClear(System.Action next=null)
    {
        StartCoroutine(Fade(1f, 0f, next));
    }

    private IEnumerator Fade(float start, float end, System.Action next)
    {
        float elapsedTime = 0f;
        Color color = image.color;

        while (elapsedTime < length)
        {
            color.a = Mathf.Lerp(start, end, elapsedTime / length);
            image.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        color.a = end;
        image.color = color;
        
        next?.Invoke();
    }
    
    private IEnumerator StayBlackCoroutine(float duration, System.Action next)
    {

        yield return new WaitForSeconds(duration);
        next?.Invoke();
    }
}

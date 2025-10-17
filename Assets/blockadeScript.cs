using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class blockadeScript : MonoBehaviour
{
    private bool isLowering = false;

    public void LowerBlockade()
    {
        if (!isLowering)
        {
            StartCoroutine(LowerBlockadeRoutine());
        }
    }

    private IEnumerator LowerBlockadeRoutine()
    {
        float loweringDistance = 3f;
        float loweringSpeed = 2f;
        isLowering = true;
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos - new Vector3(0, loweringDistance, 0);

        float elapsedTime = 0f;
        float duration = loweringDistance / loweringSpeed;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;
        gameObject.SetActive(false);
    }
}

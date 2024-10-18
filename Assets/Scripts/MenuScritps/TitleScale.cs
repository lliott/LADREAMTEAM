using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScale : MonoBehaviour
{
    [Header("Scaling Settings")]

    [Tooltip("The minimum scale factor.")]
    public Vector3 minScale = new Vector3(1f, 1f, 1f);

    [Tooltip("The maximum scale factor.")]
    public Vector3 maxScale = new Vector3(1.2f, 1.2f, 1.2f);

    [Tooltip("The speed at which scaling occurs.")]
    public float scalingSpeed = 1f;

    [Tooltip("Should the scaling start automatically?")]
    public bool startOnAwake = true;

    private RectTransform rectTransform;
    private bool scalingUp = true;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        if (startOnAwake)
        {
            StartScaling();
        }
    }

    public void StartScaling()
    {
        StopAllCoroutines();
        StartCoroutine(ScaleLoop());
    }


    public void StopScaling()
    {
        StopAllCoroutines();
    }

    private System.Collections.IEnumerator ScaleLoop()
    {
        while (true)
        {
            Vector3 targetScale = scalingUp ? maxScale : minScale;
            float elapsedTime = 0f;
            Vector3 initialScale = rectTransform.localScale;

            while (elapsedTime < scalingSpeed)
            {
                rectTransform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / scalingSpeed);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            rectTransform.localScale = targetScale;

            scalingUp = !scalingUp;
        }
    }
}

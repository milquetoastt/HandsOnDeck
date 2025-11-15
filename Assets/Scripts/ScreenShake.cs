using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    private Vector3 originalPos;
    private float shakeDuration = 0f;
    private float shakeMagnitude = 0.1f;
    private float shakeFrequency = 25f;

    private float shakeTimer = 0f;

    void Start()
    {
        originalPos = transform.localPosition;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            shakeTimer += Time.deltaTime * shakeFrequency;

            float offsetX = (Mathf.PerlinNoise(shakeTimer, 0f) - 0.5f) * 2f * shakeMagnitude;
            float offsetY = (Mathf.PerlinNoise(0f, shakeTimer) - 0.5f) * 2f * shakeMagnitude;

            transform.localPosition = originalPos + new Vector3(offsetX, offsetY, 0);

            shakeDuration -= Time.deltaTime;

            if (shakeDuration <= 0f)
            {
                transform.localPosition = originalPos;
            }
        }
    }   

    /// <summary>
    /// Call this from anywhere:
    /// ScreenShake.Instance.Shake( duration, magnitude, frequency );
    /// </summary>
    public void Shake(float duration = 0.2f, float magnitude = 0.2f, float frequency = 20f)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
        shakeFrequency = frequency;
        shakeTimer = 0f;
    }
}
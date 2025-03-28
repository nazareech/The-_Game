﻿using UnityEngine;
using Unity.Cinemachine;
using System.Collections;
using System;

public class CameraController : MonoBehaviour
{
    public static Action<float, float, float> cameraShake;
    public static Action<float> changeCameraSizeEvent;
    public static Action<Transform> changeFollowTargetEvent;

    private CinemachineCamera cam;
    //private CinemachineImpulseSource impulseSource;
    private CinemachineBasicMultiChannelPerlin noise;
    private float camSize;

    void Start()
    {
        cam = GetComponent<CinemachineCamera>();
        //impulseSource = GetComponent<CinemachineImpulseSource>();

        noise = cam.GetComponent<CinemachineBasicMultiChannelPerlin>();

        // Підписка на події
        cameraShake += Shake;
        changeCameraSizeEvent += ChangeCameraSize;
        changeFollowTargetEvent += ChangeFollowTarget;

    }

    void OnDestroy()
    {
        // Відписка від подій
        cameraShake -= Shake;
        changeCameraSizeEvent -= ChangeCameraSize;
        changeFollowTargetEvent -= ChangeFollowTarget;

    }

    // Функція для тряски камери
    public void Shake(float amplitude, float frequency, float duration)
    {
        if (noise == null) return;

        StopAllCoroutines();
        StartCoroutine(ShakeCoroutine(amplitude, frequency, duration));
        StartCoroutine(StopShake(duration));
    }

    private IEnumerator ShakeCoroutine(float amplitude, float frequency, float duration)
    {
        noise.AmplitudeGain = amplitude;
        noise.FrequencyGain = frequency;

        yield return new WaitForSeconds(duration);

        noise.AmplitudeGain = 0f;
        noise.FrequencyGain = 0f;
    }

    
    // 🔹 Корутин для зупинки тряски камери через певний час
    private IEnumerator StopShake(float duration)
    {
        yield return new WaitForSeconds(duration);
        noise.AmplitudeGain = 0f;
        noise.FrequencyGain = 0f;
    }

    // 🔹 Зміна розміру огляду камери
    void ChangeCameraSize(float newSize)
    {
        StopAllCoroutines();
        camSize = cam.Lens.OrthographicSize;
        StartCoroutine(ChangeSize(newSize));
    }

    // 🔹 Зміна об'єкта відстеження камерою
    void ChangeFollowTarget(Transform followObject)
    {
        if (followObject != null) cam.Follow = followObject;
    }

    // 🔹 Корутин для плавного зміни розміру камери
    private IEnumerator ChangeSize(float newSize)
    {
        if (cam.Lens.OrthographicSize == newSize) yield break;

        for (float i = 0; i < 1f; i += Time.deltaTime)
        {
            cam.Lens.OrthographicSize = Mathf.Lerp(camSize, newSize, EaseInOut(i));
            yield return null;
        }
    }

    // 🔹 Функція для плавного переходу
    private float EaseInOut(float t)
    {
        return t < 0.5f ? 2 * t * t : -1 + (4 - 2 * t) * t;
    }
}





/*
    Виклик методів з інших скриптів:

 *   
 // Виклик тряски камери
        CameraController.cameraShake?.Invoke(strength, time, fadeTime);
 
 *   
// Зміна розміру камери
        CameraController.changeCameraSizeEvent?.Invoke(newSize);
 
 *   
// Зміна об'єкта відстеження
        CameraController.changeFollowTargetEvent?.Invoke(newTarget);
 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }

    private CinemachineVirtualCamera cam;
    private float shakeTimer;

    private void Awake() {
        Instance = this;
        cam = GetComponent<CinemachineVirtualCamera>();
        
    }

    public void ShakeCamera( float intensity, float time ) {

        CinemachineBasicMultiChannelPerlin camPerlin = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        camPerlin.m_AmplitudeGain = intensity;
        shakeTimer = time;

    }

    private void Update() {

        if (shakeTimer > 0) {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f) {
                // Time Over!
                CinemachineBasicMultiChannelPerlin camPerlin = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                camPerlin.m_AmplitudeGain = 0f;

            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

    private CinemachineFramingTransposer framingTransposer;

    private void Awake()
    {
        Instance = this;
    }

    public void Setup()
    {
        framingTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    public void SetupGameCamera()
    {
        StartCoroutine(CameraMoverY(false));
    }

    public void SetupMenuCamera()
    {
        StartCoroutine(CameraMoverY(true));
    }

    private IEnumerator CameraMoverY(bool up)
    {
        float timer = 0;

        float initialValue = framingTransposer.m_ScreenY;
        float targetValue;

        if (up)
        {
            targetValue = Constants.cameraNormalY;
        }
        else
        {
            targetValue = Constants.cameraIngameY;
        }

        while(timer < 1)
        {
            timer += Time.deltaTime / Constants.transitionTime;

            framingTransposer.m_ScreenY = Mathf.Lerp(initialValue, targetValue, timer);

            yield return new WaitForEndOfFrame();
        }
    }
}

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

    public void Shake(float duration, float intensity)
    {

    }

    private IEnumerator CameraMoverY(bool up)
    {
        float timer = 0;

        float initialValueY = framingTransposer.m_ScreenY;
        float targetValueY;

        float initialValueDist = framingTransposer.m_CameraDistance;
        float targetValueDist;

        if (up)
        {
            targetValueY = Constants.cameraNormalY;
            targetValueDist = Constants.cameraNormalDist;
        }
        else
        {
            targetValueY = Constants.cameraIngameY;
            targetValueDist = Constants.cameraInGameDist;
        }

        while(timer < 1)
        {
            timer += Time.deltaTime / Constants.transitionTime;

            framingTransposer.m_ScreenY = Mathf.Lerp(initialValueY, targetValueY, Mathf.SmoothStep(0, 1, timer));
            framingTransposer.m_CameraDistance = Mathf.Lerp(initialValueDist, targetValueDist, Mathf.SmoothStep(0, 1, timer));

            yield return new WaitForEndOfFrame();
        }
    }
}

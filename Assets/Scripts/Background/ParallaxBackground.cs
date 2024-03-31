using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine; // 引入Cinemachine命名空间

public class ParallaxBackground : MonoBehaviour
{
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    // Separate speed ratios for X and Y directions
    public float horizontalSpeedRatio;
    public float verticalSpeedRatio;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
    }

    private void OnEnable()
    {
        CinemachineCore.CameraUpdatedEvent.AddListener(OnCameraUpdated);
    }

    private void OnDisable()
    {
        CinemachineCore.CameraUpdatedEvent.RemoveListener(OnCameraUpdated);
    }

    private void OnCameraUpdated(CinemachineBrain brain)
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;

        // Apply different speed ratios based on the direction
        Vector3 parallaxEffect = new Vector3(
            horizontalSpeedRatio * deltaMovement.x,
            verticalSpeedRatio * deltaMovement.y,
            0);

        transform.position += parallaxEffect;
        lastCameraPosition = cameraTransform.position;
    }
}

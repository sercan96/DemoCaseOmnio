using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;
    CinemachineTransposer _cinemachineComponent;

    void Start()
    {
        _cinemachineComponent = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
    }

    public void CameraMove(float cameraZAxis)
    {
        _cinemachineComponent.m_FollowOffset = new Vector3(_cinemachineComponent.m_FollowOffset.x, 
            _cinemachineComponent.m_FollowOffset.y, cameraZAxis);
    }
}

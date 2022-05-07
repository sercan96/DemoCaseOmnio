using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;
    CinemachineTransposer _cinemachineComponent;
    [SerializeField] private Vector3 gameFinishCameraPos;

    void Start()
    {
        _cinemachineComponent = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
    }

    public void PlayerCameraMovement(float cameraZAxis,float waitTime)
    {
        Vector3 newVector= new Vector3(_cinemachineComponent.m_FollowOffset.x, 
            _cinemachineComponent.m_FollowOffset.y, cameraZAxis);
        _cinemachineComponent.m_FollowOffset =Vector3.Lerp(_cinemachineComponent.m_FollowOffset, newVector, waitTime);
    }

    public void GameFinishCameraMovement()
    {
        _cinemachineComponent.m_FollowOffset = gameFinishCameraPos;
    }
}

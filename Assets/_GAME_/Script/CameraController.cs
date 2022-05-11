using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera = default;
    [SerializeField] private Vector3 _gameFinishCameraPos = default;
    
    CinemachineTransposer _cinemachineComponent;

    public bool isFast;
    void Start()
    {
        _cinemachineComponent = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
    }

    void Update()
    {
        if (isFast)
        {
            PlayerCameraMovement(2.5f, -9f, 3f);
        }
        else
        {
            PlayerCameraMovement(1.6f, -4.4f, 3f);
        }
    } 
    public void PlayerCameraMovement(float yAxis, float zAxis, float duration)
    {
        Vector3 newVector= new Vector3(_cinemachineComponent.m_FollowOffset.x, 
            yAxis, zAxis);
        
        _cinemachineComponent.m_FollowOffset =Vector3.Lerp(_cinemachineComponent.m_FollowOffset, newVector, Time.deltaTime * duration);
    }

    public void GameFinishCameraMovement()
    {
        _cinemachineComponent.m_FollowOffset = _gameFinishCameraPos;
    }
}

using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera = default;
    
    public bool isFast;
    
    private CinemachineTransposer _cinemachineTransposer;
    private CinemachineComposer _cinemachineComposer;


    void Start()
    {
        _cinemachineTransposer = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        _cinemachineComposer = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineComposer>();
  
    }

    void Update()
    {
        if (!GameManager.İnstance.isGameActive) return;
        if (isFast)
        {
            PlayerCameraMovement(2.5f, -9f, 3f);
        }
        else
        {
            PlayerCameraMovement(1.6f, -5.5f, 3f);
        }

    } 
    public void PlayerCameraMovement(float yAxis, float zAxis, float duration)
    {
        Vector3 newVector= new Vector3(_cinemachineTransposer.m_FollowOffset.x, 
            yAxis, zAxis);
        
        _cinemachineTransposer.m_FollowOffset =Vector3.Lerp(_cinemachineTransposer.m_FollowOffset, newVector, Time.deltaTime * duration);
    }

    public void GameFinishCameraMovement(Vector3 transposerOffset, Vector3 composerOffset)
    {
        _cinemachineTransposer.m_FollowOffset = transposerOffset;
        _cinemachineComposer.m_TrackedObjectOffset = composerOffset;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _smokeParticlePrefab;
    [SerializeField] private GameObject _ball;
    [SerializeField] private GameObject _uıManager;
    [SerializeField] private GameObject ConfetiParticleObject;
    [SerializeField] private CameraController _cameraController;
    
    public bool isGameActive = true;
    
    public void GameFinish(int winIndex)
    {
        _animator.SetInteger("WinIndex",winIndex);
        isGameActive = false;
        ParticleEffectActivePassive(false);
        _ball.SetActive(false);
        _uıManager.SetActive(true);
        ConfetiParticleObject.SetActive(true);
        _cameraController.GameFinishCameraMovement();
    }
    public void ParticleEffectActivePassive(bool isParticleActive)
    {
        _smokeParticlePrefab.SetActive(isParticleActive);
    }
}

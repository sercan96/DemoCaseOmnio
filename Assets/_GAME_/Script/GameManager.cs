using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Animator _animator = default;
    [SerializeField] private GameObject _smokeParticlePrefab = default;
    [SerializeField] private GameObject _ball = default;
    [SerializeField] private GameObject _gameWinPanel = default;
    [SerializeField] private GameObject ConfetiParticleObject = default;
    [SerializeField] private CameraController _cameraController = default;
    [SerializeField] private AudioClip[] _audioClips = default;
    [SerializeField] private GameObject gameOverPanel = default;
   
    
    public bool isGameActive = true;
    public static GameManager İnstance;
    
    private AudioSource _audioSource;
    
    void Awake()
    {
        İnstance = this;
        _audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        AudioPlay(0);
    
    }
    
    public void GameWin(int winIndex)
    {
        _animator.SetInteger("WinIndex",winIndex);
        isGameActive = false;
        ParticleEffectActivePassive(false);
        _ball.SetActive(false);
        _gameWinPanel.SetActive(true);
        ConfetiParticleObject.SetActive(true);
        _cameraController.GameFinishCameraMovement();
    }

    public void GameOver()
    {
        _animator.SetBool("isHit",true);
        gameOverPanel.SetActive(true);
        isGameActive = false;
        ParticleEffectActivePassive(false);
        _ball.SetActive(false);
        _cameraController.GameFinishCameraMovement();
    }
    public void ParticleEffectActivePassive(bool isParticleActive)
    {
        _smokeParticlePrefab.SetActive(isParticleActive);
    }
    public void AudioPlay(int winMusic)
    {
        if(isGameActive)
        {
            _audioSource.PlayOneShot(_audioClips[winMusic]);
        }
        else
        {
            _audioSource.Stop();
            _audioSource.PlayOneShot(_audioClips[winMusic]);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
        gameOverPanel.SetActive(false);
    }
    
}

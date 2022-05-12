using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // I did it because it requires the unity default definition.
    [SerializeField] private Animator _animator = default;
    [SerializeField] private GameObject _smokeParticlePrefab = default;
    [SerializeField] private GameObject _ball = default;
    [SerializeField] private GameObject _gameWinPanel = default;
    [SerializeField] private GameObject ConfetiParticleObject = default;
    [SerializeField] private CameraController _cameraController = default;
    [SerializeField] private AudioClip[] _audioClips = default;
    [SerializeField] private GameObject gameOverPanel = default;
    [SerializeField] private GameObject _loadingBarObject = default;
    [SerializeField] private Vector3 _transposerOffset = default;
    [SerializeField] private Vector3 _composerOffset = default;
    
    public Transform startPoint;
    public Transform endPoint;
    public Image fillAmountImage;
    public static GameManager İnstance;
    public bool isGameActive = true;
    public float currentDistance, totalDistance = 0;
    public AudioSource _audioSource;
  
    
    void Awake()
    {
        İnstance = this;
    }

    void OnEnable()
    {
        EventManager.OnFail += GameOver;
    }
    void OnDisable()
    {
        EventManager.OnFail -= GameOver;
    }
    void Start()
    {
        startPoint = GameObject.FindWithTag("Player").transform;
        endPoint = GameObject.FindWithTag("Finish").transform;
        
        totalDistance = Vector3.Distance(startPoint.position, endPoint.position);
        
        AudioPlay(0);
    
    }
    
    void Update()
    {
        currentDistance = Vector3.Distance(startPoint.transform.position, endPoint.transform.position);
        fillAmountImage.fillAmount = 1 - (currentDistance / totalDistance);
    }
    
    public void GameWin(int winIndex)
    {
        _animator.SetInteger("WinIndex",winIndex);
        isGameActive = false;
        ParticleEffectActivePassive(false);
        _ball.SetActive(false);
        _gameWinPanel.SetActive(true);
        ConfetiParticleObject.SetActive(true);
        _cameraController.GameFinishCameraMovement(_transposerOffset,_composerOffset);
        _loadingBarObject.SetActive(false);
        
    }

    public void GameOver()
    {
        _animator.SetBool("isHit",true);
        gameOverPanel.SetActive(true);
        isGameActive = false;
        ParticleEffectActivePassive(false);
        _ball.SetActive(false);
        _cameraController.GameFinishCameraMovement(_transposerOffset,_composerOffset);
        _loadingBarObject.SetActive(false);
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

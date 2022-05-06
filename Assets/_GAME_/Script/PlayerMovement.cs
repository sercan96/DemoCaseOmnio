using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Cinemachine;
using DG.Tweening;
using Unity.Collections;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private GameObject _smokeParticlePrefab;
    [SerializeField] private GameObject _ball;
    [SerializeField] private GameObject _uıManager;
    [SerializeField] private int _speed;
    
    private Quaternion _defaultRotate;
    private CharacterController _characterController;
    private Animator _animator;
    private Animation _animation;
    private float _defaultRotation;
    private float rotationPositiveZAxis = 20f;
    private float rotationNegativeZAxis = -20f;
    private bool _isMouseClick = true;
    private bool _isMousePress;
    private bool _isGameActive = true;

    void Awake()
    {
         _speed = 5;
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _defaultRotation = transform.rotation.z;
        _defaultRotate =Quaternion.Euler(transform.rotation.x,transform.rotation.y,_defaultRotation);
    }
    
    void FixedUpdate()
    {
        if (_isGameActive)
        {
            Move(Vector3.forward * Time.fixedDeltaTime * _speed);
            PlayerDirection(-1,1);
        }

    }
    void Move(Vector3 moveVector)
    {
        _characterController.Move(moveVector);
    }

    void PlayerDirection(float negativeMoveXAxis, float positiveMoveXAxis)
    {
        if (Input.GetMouseButton(0))
        {
            RunFast(true,15);
            _isMousePress = true;
            Vector3 position = new Vector3(negativeMoveXAxis, transform.position.y, transform.position.z);
            transform.DOMove(position, .5f);
            PlayerRotation(rotationPositiveZAxis);
            ParticleEffectActivePassive(true);
            _cameraController.CameraMove(-5);
        }
        else
        {
 
            RunFast(false,10);
            _isMouseClick = true;
            _cameraController.CameraMove(-5);
            ParticleEffectActivePassive(false);
            Vector3 position = new Vector3(positiveMoveXAxis, transform.position.y, transform.position.z);
            transform.DOMove(position, .5f);
            if (_isMousePress)
            {
                PlayerRotation(rotationNegativeZAxis);
            }
            _isMousePress = false;
        }
    }

    private void PlayerRotation(float rotationAxis)
    {
        if (_isMouseClick)
        {
            Quaternion rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y,  rotationAxis);
            transform.DORotateQuaternion(rotation, .2f).OnComplete(() =>
            {
                transform.DORotateQuaternion(_defaultRotate,.2f);
            }); 
        }
        _isMouseClick = false; // Bir kere çalışsın.
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GameFinish"))
        {
            GameFinish(Random.Range(1,4));
        }
    }

    void GameFinish(int winIndex)
    {
        _animator.SetInteger("WinIndex",winIndex);
        _isGameActive = false;
        ParticleEffectActivePassive(false);
        _ball.SetActive(false);
        _uıManager.SetActive(true);
    }

    private void RunFast(bool isRun,int newSpeed)
    {
        _animator.SetBool("isRun",isRun);
        _speed = newSpeed;
    }

    private void ParticleEffectActivePassive(bool isParticleActive)
    {
        _smokeParticlePrefab.SetActive(isParticleActive);
    }
    
}


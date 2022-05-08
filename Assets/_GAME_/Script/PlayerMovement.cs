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

    [SerializeField] private CameraController _cameraController = default;
    [SerializeField] private int _speed;
    [SerializeField] private GameManager _gameManager = default;
    
    private Quaternion _defaultRotate;
    private CharacterController _characterController;
    private Animator _animator;
    private Animation _animation;
    
    private float _defaultRotation;
    private float rotationPositiveZAxis = 22f;
    private float rotationNegativeZAxis = -22f;
    private bool _isMouseClick = true;
    private bool _isMousePress;
    // private Rigidbody rb;

    void Awake()
    {
        // rb = GetComponent<Rigidbody>();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _defaultRotation = transform.rotation.z;
        _defaultRotate =Quaternion.Euler(transform.rotation.x,transform.rotation.y,_defaultRotation);
        _speed = 5;

    }
    
    void FixedUpdate()
    {
        if (_gameManager.isGameActive)
        {
            Move(Vector3.forward * Time.fixedDeltaTime * _speed);
            // rb.AddForce(-Vector3.forward * Time.fixedDeltaTime * _speed);
            PlayerDirection(-1,1);
        }

    }
    public void Move(Vector3 moveVector)
    {
        _characterController.Move(moveVector);
        // rigidbody.AddForce(-Vector3.forward * Time.fixedDeltaTime * _speed);
    }

    void PlayerDirection(float negativeMoveXAxis, float positiveMoveXAxis)
    {
        if (Input.GetMouseButton(0))
        {
            RunFast(true, 15);
            _isMousePress = true;
            Vector3 position = new Vector3(negativeMoveXAxis, transform.position.y, transform.position.z);
            transform.DOMove(position, 1f);
            PlayerRotation(rotationPositiveZAxis);
            _gameManager.ParticleEffectActivePassive(true);
            _cameraController.PlayerCameraMovement(-5, 10f);

        }
        else
        {
            RunFast(false,10);
            _isMouseClick = true;
            _cameraController.PlayerCameraMovement(-5,10f);
            _gameManager.ParticleEffectActivePassive(false);
            Vector3 position = new Vector3(positiveMoveXAxis, transform.position.y, transform.position.z);
            transform.DOMove(position, 1f);
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
    
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("enemy"))
        {
            Debug.Log("carpıstı");
            Rigidbody body = hit.collider.attachedRigidbody;
            // no rigidbody
            if (body == null || body.isKinematic)
                return;
            Debug.Log("carpıstı");
        }
        else if (hit.gameObject.tag != "GameFinishCollider") return;
        Debug.Log("girdim");
        _gameManager.GameFinish(Random.Range(1,4));
        _gameManager.AudioPlay(Random.Range(1,3));
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.gameObject.CompareTag("enemy"))
    //     {
    //         Debug.Log("carpıstı");
    //     }
    //     else if (other.gameObject.tag != "GameFinishCollider") return;
    //     Debug.Log("girdim");
    //     _gameManager.GameFinish(Random.Range(1,4));
    //     _gameManager.AudioPlay(Random.Range(1,3));
    // }



    private void RunFast(bool isRun,int newSpeed)
    {
        _animator.SetBool("isRun",isRun);
        _speed = newSpeed;
    }
    

    
    
}


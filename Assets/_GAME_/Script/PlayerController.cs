using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Cinemachine;
using DG.Tweening;
using Unity.Collections;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngineInternal;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private CameraController _cameraController = default;
    [SerializeField] private int _moveSpeed;
    [SerializeField] private GameManager _gameManager = default;
    [SerializeField] private int _fastSpeed = 100;
    [SerializeField] private int _slowSpeed = 70;
    
    private Quaternion _defaultRotate;
    private CharacterController _characterController;
    private Animator _animator;
    private Animation _animation;
    
    private float _defaultRotation;
    private float rotationPositiveZAxis = 20f;
    private float rotationNegativeZAxis = -20f;
    private bool _isMouseClick = true;
    private bool _isMousePress;
    private int negativeMoveXAxis = -1;
    private int positiveMoveXAxis = 1;


    private Rigidbody rb;
    void Awake()
    {
         rb = GetComponent<Rigidbody>();
        // _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _defaultRotation = transform.rotation.z;
        _defaultRotate =Quaternion.Euler(transform.rotation.x,transform.rotation.y,_defaultRotation);
        _moveSpeed = 70;
    }
    
    void FixedUpdate()
    {
        if (_gameManager.isGameActive)
        {
            Move(Vector3.forward * Time.fixedDeltaTime * _moveSpeed);
            PlayerControl();
        }

    }
    
    public void Move(Vector3 moveVector)
    {
        // _characterController.Move(moveVector);
        // _characterController.center = _characterController.center;
        // rb.MovePosition(moveVector);
        transform.Translate(moveVector);
    }

    void PlayerControl()
    {
        if (Input.GetMouseButton(0))
        {
            if (_isMouseClick)
            {
                PlayerRotation(rotationPositiveZAxis);
            }
            PassLeftOrRightSide(negativeMoveXAxis);
            RunFastOrSlow(true,_fastSpeed);
            _gameManager.ParticleEffectActivePassive(true);
            _cameraController.PlayerCameraMovement(-5, 10f);
            return;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isMouseClick = true;
            PlayerRotation(rotationNegativeZAxis);
        }
        PassLeftOrRightSide(positiveMoveXAxis);
        RunFastOrSlow(false,_slowSpeed);
        _cameraController.PlayerCameraMovement(-5,10f);
        _gameManager.ParticleEffectActivePassive(false);
       
        _isMouseClick = true;
        }

    private void PlayerRotation(float rotationAxis)
    {
        if (_isMouseClick)
        {
            Quaternion rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y,  rotationAxis);
            transform.DORotateQuaternion(rotation,.25f).OnComplete(() =>
            {
                transform.DORotateQuaternion(_defaultRotate,.45f);
            }); 
        }
        _isMouseClick = false; // Bir kere çalışsın.
    }
    
    
    private void RunFastOrSlow(bool isRun,int newSpeed)
    {
        _animator.SetBool("isRun",isRun);
        _moveSpeed = newSpeed;
    }

    private void PassLeftOrRightSide(float moveXAxis)
    {
        Vector3 position = new Vector3(moveXAxis, transform.position.y, transform.position.z);
        transform.DOMove(position, 1.1f);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            Debug.Log("carpıstı");
        }
        else if (other.gameObject.CompareTag("GameFinishCollider"))
        {
            Debug.Log("girdim");
            _gameManager.GameFinish(Random.Range(1,4));
            _gameManager.AudioPlay(Random.Range(1,3));
        }
 
    }

    // private  void OnControllerColliderHit(ControllerColliderHit hit)
    // {
    //     if (hit.gameObject.tag == "enemy")
    //     {
    //         Debug.Log("carpıstı");
    //     }
    //     
    //     
    //     else if (hit.gameObject.tag == "GameFinishCollider")
    //     {
    //         Debug.Log("girdim");
    //         _gameManager.GameFinish(Random.Range(1,4));
    //         _gameManager.AudioPlay(Random.Range(1,3));
    //     }
    // }
    

    

    

    

    
    
}
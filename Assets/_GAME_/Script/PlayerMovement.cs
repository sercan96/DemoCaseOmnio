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
    private int negativeMoveXAxis = -1;
    private int positiveMoveXAxis = 1;
    private int fastSpeed = 15;
    private int slowSpeed = 10;

    private bool isMove;

    void Awake()
    {
        // rb = GetComponent<Rigidbody>();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _defaultRotation = transform.rotation.z;
        _defaultRotate = Quaternion.Euler(transform.rotation.x, transform.rotation.y, _defaultRotation);
        _speed = 5;
    }

    void FixedUpdate()
    {
        if (_gameManager.isGameActive)
        {
            Move(Vector3.forward * Time.fixedDeltaTime * _speed);
        }
    }

    void Update()
    {
        PlayerControl();
    }

    public void Move(Vector3 moveVector)
    {
        if(isMove) return;
        _characterController.Move(moveVector);
    }

    void PlayerControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMove = true;
            PlayerRotation(rotationPositiveZAxis);
            RunFastOrSlow(true, fastSpeed);
            PassLeftOrRightSide(negativeMoveXAxis);
            _gameManager.ParticleEffectActivePassive(true);
            //_cameraController.PlayerCameraMovement(-5, 10f);
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMove = true;
            Debug.Log("up");
            PlayerRotation(rotationNegativeZAxis);
            RunFastOrSlow(false, slowSpeed);
            PassLeftOrRightSide(positiveMoveXAxis);
            _gameManager.ParticleEffectActivePassive(false);
            //_cameraController.PlayerCameraMovement(-5, 10f);
        }
        
    }

    private void PlayerRotation(float rotationAxis)
    {
        Quaternion rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rotationAxis);
        transform.DORotateQuaternion(rotation, .3f).OnComplete(() =>
        {
            transform.DORotateQuaternion(_defaultRotate, .3f);
        });
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "enemy")
        {
            Debug.Log("carpıstı");
        }


        else if (hit.gameObject.tag == "GameFinishCollider")
        {
            Debug.Log("girdim");
            _gameManager.GameFinish(Random.Range(1, 4));
            _gameManager.AudioPlay(Random.Range(1, 3));
        }
    }

    private void RunFastOrSlow(bool isRun, int newSpeed)
    {
        _animator.SetBool("isRun", isRun);
        _speed = newSpeed;
    }

    private void PassLeftOrRightSide(float moveXAxis)
    {
        Vector3 position = new Vector3(moveXAxis, transform.position.y, transform.position.z);
        transform.DOMove(position, .3f).OnComplete((() => isMove = false));
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
}
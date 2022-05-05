using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using Unity.Collections;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private int speed = 2;

    private Quaternion _defaultRotate;
    private CharacterController _characterController;
    
    private float _defaultRotation;
    private float rotationPositiveZAxis = 25f;
    private float rotationNegativeZAxis = -25f;
    private bool _isMouseClick = true;
    private bool _isMousePress;
    


    void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _defaultRotation = transform.rotation.z;
        _defaultRotate =Quaternion.Euler(transform.rotation.x,transform.rotation.y,_defaultRotation);
    }
    
    void FixedUpdate()
    {
        Move(Vector3.forward * Time.fixedDeltaTime * speed);
        PlayerDirection(-1,0);
    }
    void Move(Vector3 moveVector)
    {
        _characterController.Move(moveVector);
    }

    void PlayerDirection(float negativeMoveXAxis, float positiveMoveXAxis)
    {
        if (Input.GetMouseButton(0))
        {
            _isMousePress = true;
            Vector3 position = new Vector3(negativeMoveXAxis, transform.position.y, transform.position.z);
            transform.DOMove(position, .5f);
            PlayerRotation(rotationPositiveZAxis);
            _cameraController.CameraMove(-6);
        }
        else
        {
            _isMouseClick = true;
            _cameraController.CameraMove(-5);
            // transform.DOKill();
            Vector3 position = new Vector3(positiveMoveXAxis, transform.position.y, transform.position.z);
            transform.DOMove(position, .5f);
            if (_isMousePress)
            {
                PlayerRotation(rotationNegativeZAxis);
            }
            _isMousePress = false;
        }
    }

    public void PlayerRotation(float rotationAxis)
    {
        if (_isMouseClick)
        {
            Quaternion rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y,  rotationAxis);
            transform.DORotateQuaternion(rotation, .3f).OnComplete(() =>
            {
                transform.DORotateQuaternion(_defaultRotate,.3f);
            }); 
        }
        _isMouseClick = false; // Bir kere çalışsın.
    }
}

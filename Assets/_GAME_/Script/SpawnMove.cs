using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMove : MonoBehaviour
{
    [SerializeField] private bool _isAmigo;
    [SerializeField] private bool _isEnemy;
    [SerializeField]private float _moveSpeed = 10f;
    
    
    private Rigidbody _rigidbody;
 

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (_isEnemy) Move(transform.position + (Vector3.back * Time.fixedDeltaTime * _moveSpeed));

    }   

    void Move(Vector3 moveVector)
    {
        _rigidbody.MovePosition(moveVector);
    }

}

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpawnMove : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    public float GetMoveSpeed
    {
        get => _moveSpeed;
        set => _moveSpeed = value;
    }

    private Rigidbody _rigidbody;
 

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
       Move();
    }   

    void Move()
    {
        _rigidbody.MovePosition(transform.position + (Vector3.back * Time.fixedDeltaTime) * _moveSpeed);
    }
    

}

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AmigoMove : MonoBehaviour
{
    [SerializeField] private float finishMoveSpeed;
    private Rigidbody _rigidbody;
    
    void OnEnable()
    {
        EventManager.OnFail += ChangeMoveSpeed;
        EventManager.OnWin += DestroyObject;
    }
    void OnDisable()
    {
        EventManager.OnFail -= ChangeMoveSpeed;
        EventManager.OnWin -= DestroyObject;
    }
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
        _rigidbody.MovePosition(transform.position + (Vector3.back * Time.fixedDeltaTime) * MoveSpeed.instance.amigoSpeed);
    }
    
    public void ChangeMoveSpeed()
    {
        MoveSpeed.instance.amigoSpeed = finishMoveSpeed;
    }
    
    public void DestroyObject()
    {
        Destroy(gameObject);
    }
    

}

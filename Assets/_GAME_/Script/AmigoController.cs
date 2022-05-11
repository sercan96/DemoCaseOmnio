using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AmigoController : MonoBehaviour
{
    private SpawnControl _spawnControl;
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
        _spawnControl = FindObjectOfType<SpawnControl>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
       Move();

       if (_spawnControl._enemySpawnPosition.position.z < transform.position.z)
       {
           gameObject.SetActive(false);
       }
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

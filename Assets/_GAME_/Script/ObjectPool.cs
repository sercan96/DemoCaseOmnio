using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private CharacterController _characterController;
    [SerializeField] private float _speed;
    private Rigidbody rb;

    void Start()
    {
        //_characterController = GetComponent<CharacterController>();
         rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        EnemyController();
    }
    public void EnemyController()
    {
       //_characterController.Move(-Vector3.forward * Time.fixedDeltaTime * _speed);
        rb.AddForce(-Vector3.forward * Time.fixedDeltaTime * _speed);
    }
    // private void OnControllerColliderHit(ControllerColliderHit hit)
    // {
    //     if (hit.gameObject.CompareTag("Player"))
    //     {
    //         Debug.Log("carpıstı");
    //         // _animator.SetBool("isHit",true);            
    //     }
    // }
    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.gameObject.CompareTag("Player"))
    //     {
    //         Debug.Log("carpıstı");
    //     }
    //     else if (other.gameObject.tag != "GameFinishCollider") return;
    //     Debug.Log("girdim");
    //
    //
    // }

 
}

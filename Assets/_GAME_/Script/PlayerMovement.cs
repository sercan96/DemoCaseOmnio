using DG.Tweening;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private MoveMap moveMap;
    private Rigidbody _rigidbody;

    public float _moveSpeed;
    [SerializeField] private float SlowMoveSpeed;
    [SerializeField] private float FastMoveSpeed;

    [SerializeField] private CameraController _cameraController = default;
    [SerializeField] private GameManager _gameManager = default;

    private Quaternion _defaultRotate;
    private CharacterController _characterController;
    private Animator _animator;
    private Animation _animation;

    private float _defaultRotation;
    private float rotationPositiveZAxis = 22f;
    private float rotationNegativeZAxis = -22f;
    private bool _isMouseClick = true;
    
    // private Rigidbody rb;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _defaultRotation = transform.rotation.z;
        _defaultRotate = Quaternion.Euler(transform.rotation.x, transform.rotation.y, _defaultRotation);
    }

    void FixedUpdate()
    {
        // if (_gameManager.isGameActive)
        // {
        //     //Move(Vector3.forward * Time.fixedDeltaTime * _speed);
        //     //PlayerManager();
        //     Move();
        // }
    }

    private void Move()
    {
        //_rigidbody.MovePosition(transform.position + Vector3.forward * (_moveSpeed * Time.deltaTime));
        //_characterController.Move(Vector3.forward * (_moveSpeed * Time.deltaTime));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _gameManager.ParticleEffectActivePassive(true);
            moveMap.moveSpeed = FastMoveSpeed;
            _animator.SetBool("isRun", true);
            PassLeftOrRightSide(-1);
            PlayerRotation(rotationPositiveZAxis);
        }

        if (Input.GetMouseButtonUp(0))
        {
            PassLeftOrRightSide(1);
            _gameManager.ParticleEffectActivePassive(false);
            PlayerRotation(rotationNegativeZAxis);
            moveMap.moveSpeed = SlowMoveSpeed;
            _animator.SetBool("isRun", false);
        }

        moveMap.moveSpeed = SlowMoveSpeed;
    }

    private void PlayerRotation(float rotationAxis)
    {
        Quaternion rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rotationAxis);
        transform.DORotateQuaternion(rotation, .3f).OnComplete(() =>
        {
            transform.DORotateQuaternion(_defaultRotate, .3f);
        });
    }
    private void PassLeftOrRightSide(float moveXAxis)
    {
        
        Vector3 position = new Vector3(moveXAxis, transform.position.y, transform.position.z);
        //transform.position = Vector3.Lerp(transform.position, position, 20f);
        transform.DOMove(position, 1f);
    }

    // private void OnControllerColliderHit(ControllerColliderHit hit)
    // {
    //     if (hit.gameObject.CompareTag("enemy"))
    //     {
    //         Debug.Log("carpıstı");
    //         Rigidbody body = hit.collider.attachedRigidbody;
    //         // no rigidbody
    //         if (body == null || body.isKinematic)
    //             return;
    //         Debug.Log("carpıstı");
    //     }
    //     else if (hit.gameObject.tag == "GameFinishCollider")
    //     {
    //         Debug.Log("girdim");
    //         _gameManager.GameFinish(Random.Range(1, 4));
    //         _gameManager.AudioPlay(Random.Range(1, 3));
    //     // }
    // }



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
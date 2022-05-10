using DG.Tweening;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private MoveMap moveMap;
    private Rigidbody _rigidbody;
    
    [SerializeField] private float SlowMoveSpeed;
    [SerializeField] private float FastMoveSpeed;
    [SerializeField] private SpawnMove[] _spawnMove;
    [SerializeField] private CameraController _cameraController = default;

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
        if (!GameManager.İnstance.isGameActive) return;
        if (Input.GetMouseButtonDown(0))
        {
            GameManager.İnstance.ParticleEffectActivePassive(true);
            moveMap.moveSpeed = FastMoveSpeed;
            _animator.SetBool("isRun", true);
            PassLeftOrRightSide(-1);
            PlayerRotation(rotationPositiveZAxis);
        }

        if (Input.GetMouseButtonUp(0))
        {
            PassLeftOrRightSide(1);
            GameManager.İnstance.ParticleEffectActivePassive(false);
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
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            GameManager.İnstance.isGameActive = false;
        
            // _spawnMove[0].GetMoveSpeed = _spawnMove[0].GetMoveSpeed / 2;
            // _spawnMove[1].GetMoveSpeed = _spawnMove[1].GetMoveSpeed * 2;
    

        }
        else if (other.gameObject.tag == "GameFinishCollider")
        {
            Debug.Log("girdim");
        }
        GameManager.İnstance.GameFinish(Random.Range(1,4));
        GameManager.İnstance.AudioPlay(Random.Range(1,3));

    }
}
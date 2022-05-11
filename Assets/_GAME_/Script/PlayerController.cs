using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private float _slowMoveSpeed = 10;
    [SerializeField] private float _fastMoveSpeed = 50;

    private Quaternion _defaultRotate;
    private Animator _animator;
    private Animation _animation;

    private float _defaultRotation;
    private float rotationPositiveZAxis = 22f;
    private float rotationNegativeZAxis = -22f;

    
    void Awake()
    {
        _animator = GetComponent<Animator>();
        _defaultRotation = transform.rotation.z;
        _defaultRotate = Quaternion.Euler(transform.rotation.x, transform.rotation.y, _defaultRotation);
    }

    void Start()
    {
        MoveSpeed.instance.planeSpeed = _slowMoveSpeed;
    }
    
    void Update()
    {
        if (!GameManager.İnstance.isGameActive) return;
        if (Input.GetMouseButtonDown(0))
        {
            GameManager.İnstance.ParticleEffectActivePassive(true);
            MoveSpeed.instance.planeSpeed = _fastMoveSpeed;
            _animator.SetBool("isRun", true);
            PassLeftOrRightSide(-1);
            PlayerRotation(rotationPositiveZAxis);
            _cameraController.isFast = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            PassLeftOrRightSide(1);
            GameManager.İnstance.ParticleEffectActivePassive(false);
            PlayerRotation(rotationNegativeZAxis);
            MoveSpeed.instance.planeSpeed = _slowMoveSpeed;
            _animator.SetBool("isRun", false);
            _cameraController.isFast = false;
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
    private void PassLeftOrRightSide(float moveXAxis)
    {
        Vector3 position = new Vector3(moveXAxis, transform.position.y, transform.position.z);
        transform.DOMove(position, 1f);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            EventManager.TriggerOnFail();
            GetComponent<BoxCollider>().enabled = false;
            GameManager.İnstance.AudioPlay(2); // 4.indexi gireceğim. !!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        }
        else if (other.gameObject.tag == "GameFinishCollider")
        {
            EventManager.TriggerOnWin();

            GameManager.İnstance.GameWin(Random.Range(1,4));
            GameManager.İnstance.AudioPlay(Random.Range(1,3));
        }

    }
}
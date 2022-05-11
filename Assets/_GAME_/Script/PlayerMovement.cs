using DG.Tweening;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Sürümden dolayı [Serializefield] iel tanımlanan değerlerin default girilmesi uyarısı alındığı için bu şekilde girdim.
    [SerializeField] private MoveMap moveMap = default;
    [SerializeField] private float _slowMoveSpeed = 10;
    [SerializeField] private float _fastMoveSpeed = 50;
    // [SerializeField] private SpawnMove[] _spawnMove = default;

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
        moveMap.moveSpeed = _slowMoveSpeed;
    }
    
    void Update()
    {
        if (!GameManager.İnstance.isGameActive) return;
        if (Input.GetMouseButtonDown(0))
        {
            GameManager.İnstance.ParticleEffectActivePassive(true);
            moveMap.moveSpeed = _fastMoveSpeed;
            _animator.SetBool("isRun", true);
            PassLeftOrRightSide(-1);
            PlayerRotation(rotationPositiveZAxis);
            return;
        }

        if (Input.GetMouseButtonUp(0))
        {
            PassLeftOrRightSide(1);
            GameManager.İnstance.ParticleEffectActivePassive(false);
            PlayerRotation(rotationNegativeZAxis);
            moveMap.moveSpeed = _slowMoveSpeed;
            _animator.SetBool("isRun", false);
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
        //transform.position = Vector3.Lerp(transform.position, position, 20f);
        transform.DOMove(position, 1f);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            GameManager.İnstance.GameOver();
            GetComponent<BoxCollider>().enabled = false;
            GameManager.İnstance.AudioPlay(2); // 4.indexi gireceğim. !!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            // _spawnMove[0].GetMoveSpeed = _spawnMove[0].GetMoveSpeed / 2;
            // _spawnMove[1].GetMoveSpeed = _spawnMove[1].GetMoveSpeed * 2;

        }
        else if (other.gameObject.tag == "GameFinishCollider")
        {
            Debug.Log("girdim");
            GameManager.İnstance.GameWin(Random.Range(1,4));
            GameManager.İnstance.AudioPlay(Random.Range(1,3));
        }

    }
}
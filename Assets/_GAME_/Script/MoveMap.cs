using UnityEngine;

public class MoveMap : MonoBehaviour
{
    public float moveSpeed;

    void Start()
    {
 
    }
        
    private void FixedUpdate()
    {
        if (!GameManager.İnstance.isGameActive) return;
        DoMoveMap();
    }

    public void DoMoveMap()
    {
        transform.Translate(Vector3.back * (moveSpeed * Time.deltaTime));
        Debug.Log(moveSpeed);
    }
}
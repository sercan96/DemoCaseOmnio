using UnityEngine;

public class MoveMap : MonoBehaviour
{
    public float moveSpeed;

    private void FixedUpdate()
    {
        if (!GameManager.İnstance.isGameActive) return;
        DoMoveMap();
    }

    public void DoMoveMap()
    {
        transform.Translate(Vector3.back * (moveSpeed * Time.deltaTime));
    }
}
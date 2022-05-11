using UnityEngine;

public class MoveMap : MonoBehaviour
{
    private void FixedUpdate()
    {
        if (!GameManager.İnstance.isGameActive) return;
        DoMoveMap();
    }

    public void DoMoveMap()
    {
        transform.Translate(Vector3.back * (MoveSpeed.instance.planeSpeed * Time.deltaTime));
    }
}
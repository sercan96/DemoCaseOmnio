using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControl : MonoBehaviour
{
    public  Transform _enemySpawnPosition;
    public  Transform _amigoSpawnPosition;
    
    void OnEnable()
    {
        EventManager.OnWin += StopSpawnInvoke;
    }
    void OnDisable()
    {
        EventManager.OnWin -= StopSpawnInvoke;
    }
    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 0, Random.Range(3,7));
        InvokeRepeating(nameof(SpawnAmigo), 0, Random.Range(1,7));
    }

    void SpawnEnemy()
    {
        ObjectPooler.instance.SpawnFromPool("Enemy", _enemySpawnPosition.position, Quaternion.Euler(0f,180f,0f));
    }

    void SpawnAmigo()
    {
        ObjectPooler.instance.SpawnFromPool("Amigo",_amigoSpawnPosition.position , Quaternion.identity);
    }

    void StopSpawnInvoke()
    {
        CancelInvoke(nameof(SpawnEnemy));
        CancelInvoke(nameof(SpawnAmigo));
    }
}

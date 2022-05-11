using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControl : MonoBehaviour
{
    public  Transform _enemySpawnPosition;
    public  Transform _amigoSpawnPosition;
    
    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 0, 2f);
        InvokeRepeating(nameof(SpawnAmigo), 0, 2f);
    }

    void SpawnEnemy()
    {
        ObjectPooler.instance.SpawnFromPool("Enemy", _enemySpawnPosition.position, Quaternion.Euler(0f,180f,0f));
    }

    void SpawnAmigo()
    {
        ObjectPooler.instance.SpawnFromPool("Amigo",_amigoSpawnPosition.position , Quaternion.identity);
    }
}

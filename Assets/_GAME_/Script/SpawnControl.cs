using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControl : MonoBehaviour
{
    [SerializeField] private Transform enemySpawnPosition;
    [SerializeField] private Transform amigoSpawnPosition;
    
    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 0, 2f);
        InvokeRepeating(nameof(SpawnAmigo), 0, 2f);
    }

    void SpawnEnemy()
    {
        ObjectPooler.instance.SpawnFromPool("Enemy", enemySpawnPosition.position, Quaternion.identity);
    }

    void SpawnAmigo()
    {
        ObjectPooler.instance.SpawnFromPool("Amigo", amigoSpawnPosition.position, Quaternion.identity);
    }
}

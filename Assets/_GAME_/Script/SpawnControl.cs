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
        InvokeRepeating(nameof(SpawnAmigo), 0, 5f);
    }

    void SpawnEnemy()
    {
        ObjectPooler.instance.SpawnFromPool("Enemy", enemySpawnPosition.position, Quaternion.Euler(0f,180f,0f));
    }

    void SpawnAmigo()
    {
        ObjectPooler.instance.SpawnFromPool("Amigo",amigoSpawnPosition.position , Quaternion.identity);
    }
}

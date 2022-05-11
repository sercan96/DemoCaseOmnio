using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpeed : MonoBehaviour
{
    public static MoveSpeed instance;

    public float enemySpeed;
    public float amigoSpeed;
    
    void Awake()
    {
        instance = this;
    }
    
    
}

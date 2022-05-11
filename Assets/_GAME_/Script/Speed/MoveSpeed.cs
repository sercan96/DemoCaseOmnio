using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpeed : MonoBehaviour
{
    public static MoveSpeed instance;
    
    public float enemySpeed;
    public float amigoSpeed;
    public float planeSpeed;
    
    void Awake()
    {
        instance = this;
    }
    
    
}

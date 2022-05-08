using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    [SerializeField] private Image Bar;
    [SerializeField] private Transform PlayerTransform;

    public void IncreaseLoadingBar()
    {
        
        Bar.fillAmount += 0.1f;
    }
    
    
    
 
    
    
    
    
    
}

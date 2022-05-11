using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static Action OnFail;
    public static Action OnWin;


    public static void TriggerOnFail()
    {
        OnFail?.Invoke();
    }
    
    public static void TriggerOnWin()
    {
        OnWin?.Invoke();
    }
}

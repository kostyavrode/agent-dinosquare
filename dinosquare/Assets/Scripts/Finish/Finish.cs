using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class Finish : MonoBehaviour
{
    public static Action onLevelPassed;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Observable.Timer(System.TimeSpan.FromSeconds(3f)).TakeUntilDisable(this).Subscribe(x => onLevelPassed?.Invoke());
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class UI : MonoBehaviour
{
    public static Action onGameStarted;
    public void StartButton()
    {
        Observable.Timer(System.TimeSpan.FromSeconds(.1f)).TakeUntilDisable(this).Subscribe(x=> onGameStarted?.Invoke());
    }
}

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        Finish.onLevelPassed += ReloadScene;
    }
    private void OnDisable()
    {
        Finish.onLevelPassed -= ReloadScene;
    }
    private void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}

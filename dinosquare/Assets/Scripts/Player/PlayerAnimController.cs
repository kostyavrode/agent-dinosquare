using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private void Awake()
    {
        PlayerNavMesh.onPlayerMove += SetRunAnimation;
        if (animator==null)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }
    private void OnDisable()
    {
        PlayerNavMesh.onPlayerMove -= SetRunAnimation;
    }
    private void SetRunAnimation()
    {
        animator.SetBool("isRunning",!animator.GetBool("isRunning"));
        Debug.Log("SetAnim");
    }
}

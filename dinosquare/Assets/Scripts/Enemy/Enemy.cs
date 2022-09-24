using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Image hpBar;
    [SerializeField] private int hp=1;
    private Rigidbody[] rigidbodies;
    private void Awake()
    {
        if(animator==null)
        {
            animator = GetComponentInChildren<Animator>();
        }
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach(Rigidbody rig in rigidbodies)
        {
            rig.isKinematic = true;
        }
        hpBar.fillAmount = hp;
    }
    private void ReceiveDamage()
    {
        hp--;
        hpBar.fillAmount = hp;
        if (hp<1)
        {
            Death();    
        }
    }
    private void Death()
    {
        GetComponent<CapsuleCollider>().enabled = false;
        LevelManager.onEnemyDeath?.Invoke();
        animator.enabled = false;
        foreach(Rigidbody rig in rigidbodies)
        {
            rig.isKinematic = false;
        }
    }
}

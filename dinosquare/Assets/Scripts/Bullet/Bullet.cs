using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletLiveTime=0.8f;
    [SerializeField] private int damage = 1;
    [SerializeField] private float speed=10f;
    private bool isReadyToFlight;
    private void Awake()
    {
        isReadyToFlight = false;
    }
    public void InitShot(Vector3 target)
    {
        if (!isReadyToFlight)
        {
            transform.LookAt(target);
            isReadyToFlight = true;
            Observable.Timer(System.TimeSpan.FromSeconds(bulletLiveTime)).TakeUntilDestroy(gameObject).Subscribe(x => Destroy(gameObject));
            Observable.EveryFixedUpdate().TakeUntilDestroy(gameObject).Subscribe(x =>
            {
                transform.Translate(Vector3.forward*speed*Time.fixedDeltaTime);
            });
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            for (int i = 0; i < damage; i++)
            {
                other.SendMessage("ReceiveDamage", SendMessageOptions.DontRequireReceiver);
            }
        }
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            return;
        }
        else
        Destroy(gameObject);
    }
}

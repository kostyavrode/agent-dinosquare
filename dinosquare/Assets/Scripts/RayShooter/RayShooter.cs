using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform bulletInstantiatePoint;
    private bool isGameStarted;
    private void Awake()
    {
        UI.onGameStarted += ChangeStartStatus;
    }
    private void OnDisable()
    {
        UI.onGameStarted -= ChangeStartStatus;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)&&isGameStarted)
        {
            Shoot();
        }
    }
    private void Shoot()
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit,200f))
        {
            CreateBullet(hit.point);
        }
    }
    private void CreateBullet(Vector3 hitPoint)
    {
        Bullet newBullet=Instantiate(bulletPrefab, bulletInstantiatePoint.position,Quaternion.identity);
        newBullet.InitShot(hitPoint);
    }
    private void ChangeStartStatus()
    {
        isGameStarted = true;
    }
}

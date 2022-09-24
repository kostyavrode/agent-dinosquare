using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using UniRx;

public class PlayerNavMesh : MonoBehaviour
{
    public static Action onPlayerMove;
    [SerializeField] private NavMeshAgent navMeshAgent;
    private void Awake()
    {
        if (navMeshAgent == null)
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
        LevelManager.onSetPlayerMovePoint += SetNavMeshDestination;
    }
    private void OnDisable()
    {
        LevelManager.onSetPlayerMovePoint -= SetNavMeshDestination;
    }
    public void SetNavMeshDestination(Vector3 point)
    {
        bool isPathEnded=false;
        navMeshAgent.SetDestination(point);
        onPlayerMove?.Invoke();
        Observable.EveryUpdate().TakeWhile(x => !isPathEnded).TakeUntilDisable(this).Subscribe(x =>
          {
              if ((navMeshAgent.pathEndPosition - navMeshAgent.transform.position).magnitude < 0.2f)
              {
                  onPlayerMove?.Invoke();
                  isPathEnded = true;
              }
          });
    }
    //private void Update()
    //{
    //    if(Input.GetButtonDown("Jump"))
    //    {
    //        RaycastHit hit;
    //        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
    //        {
    //            SetNavMeshDestination(hit.point);
    //        }
    //    }
    //}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] Collider[] _colliders;
    [SerializeField] Player _player;
    [SerializeField] int valueLayerPlayer;
    public float randomIdlestart,randomIdleend, randomRunstart, randomRunend,timeAttack;
    public Vector3 target;

    private IState currentstate;

    // Update is called once per frame
    void Update()
    {
        if(currentstate != null)
        {
            currentstate.OnExcute(this);
        }
        timeAttack += Time.deltaTime;
        CheckSphereCollider();
        
    }


    public void ChangState(IState newStatr)
    {
        if (currentstate != null)
        {
            currentstate.OnExit(this);
        }
        currentstate = newStatr;
        if(currentstate != null)
        {
            currentstate.OnEnter(this);
        }
    }
    public override void OnInit()
    {
        ChangeSkin(RandomSKin());
        ChangState(new PartrolState());
        base.OnInit();
        _agent.speed = speed;
        
        
    }
    public override void Move(Vector3 target)
    {
        _agent.SetDestination(target);
    }
    public override void OnHit(int dame)
    {
        base.OnHit(dame);
        
    }
    
    public void CheckSphereCollider()
    {
        _colliders = Physics.OverlapSphere(transform.position, 10*timeDespawnBullet);
        Array.Sort(_colliders, Compare);
        for(int i = 0; i< _colliders.Length; i++)
        {
            if( _colliders[i].gameObject.layer == valueLayerPlayer)
            {
                Vector3 dir = _colliders[i].transform.position - transform.position;
                dir.Normalize();
                if (Physics.Raycast(transform.position, dir, Getradius())){
                    target = dir;
                }
            }
        }
    }
    public int Compare(Collider x, Collider y)//sap xep mang hitcollider theo distance uu tien tu gan den xa
    {
        float distanceX = Vector3.Distance(x.transform.position, transform.position);
        float distanceY = Vector3.Distance(y.transform.position, transform.position);

        if (distanceX < distanceY)
        {
            return -1;
        }
        else if (distanceX > distanceY)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
    private void LateUpdate()
    {
        Vector3 newvt = _playerDirection();
        newvt.Normalize();
        Quaternion newQt = Quaternion.LookRotation(newvt);
        upperBody.transform.rotation = Quaternion.Slerp(upperBody.transform.rotation, newQt, 5 * Time.deltaTime);
        if (isAttack)
        {
            Attack(itemskin.poolType,upperBody.transform.forward);
            isAttack = false;
            SetTimeAttack();
        }
    }
    public override void Die()
    {
        base.Die();
    }
    public override void Attack(PoolType type,Vector3 dir)
    {
        base.Attack(type, dir);

        target = Vector3.zero;
    }

    public override void OnDeSpawn()
    {
        base.OnDeSpawn();
    }

    
    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawSphere(transform.position, 10 * (timeDespawnBullet+1f));
    //}

    public float Getradius()
    {
        return 10 * (timeDespawnBullet + 1f);
    }

    public Vector3 _playerDirection()
    {
        Vector3 dir = _player.transform.position - transform.position;
        return dir;
    }
    public bool IsAttack()
    {
        if(timeAttack > 2f)
        {
            return true;
        }
        return false;
        
    }
    public void SetTimeAttack()
    {
        timeAttack = 0;
    }
}

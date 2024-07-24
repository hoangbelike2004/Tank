using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] Collider[] _colliders;
    [SerializeField] Player _player;
    [SerializeField] int valueLayerPlayer, valueLayerBot;
    [SerializeField] private LayerMask _layerWall, LayerPlayer;
    public float randomIdlestart, randomIdleend, randomRunstart, randomRunend, timeAttack;
    public Vector3 target;

    private IState currentstate;
    public bool isCanShoot;
    RaycastHit hit;
    // Update is called once per frame
    void Update()
    {
        if (!isPlay || isDie) return;
        if (currentstate != null)
        {
            currentstate.OnExcute(this);
        }
        timeAttack += Time.deltaTime;

        CheckSphereCollider();


        

        Quaternion newQt = Quaternion.LookRotation(target);
        upperBody.transform.rotation = Quaternion.Slerp(upperBody.transform.rotation, newQt, 0.5f);
        if (isCanShoot && isAttack && !Physics.Raycast(upperBody.transform.position + new Vector3(0, 0, 1.2f), upperBody.transform.forward * 10f + Vector3.up, _layerWall))
        {

            Attack(itemskin.poolType, upperBody.transform.forward);
            isAttack = false;
            SetTimeAttack();


        }

        //if (Physics.Raycast(upperBody.transform.position + new Vector3(0, 0, 2), upperBody.transform.forward * 10f + Vector3.up, _layerWall))
        //{
        //    Debug.Log(1);
        //}
        //Debug.Log(0);


    }


    public void ChangState(IState newStatr)
    {
        if (currentstate != null)
        {
            currentstate.OnExit(this);
        }
        currentstate = newStatr;
        if (currentstate != null)
        {
            currentstate.OnEnter(this);
        }
    }
    public override void OnInit()
    {
        ChangeSkin(RandomSKin());
        ChangState(new IdleState());
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
        _colliders = Physics.OverlapSphere(transform.position, 20);
        Array.Sort(_colliders, Compare);
        for (int i = 0; i < _colliders.Length; i++)
        {
            if (_colliders[i].gameObject.layer == valueLayerPlayer || _colliders[i].gameObject.layer == valueLayerBot)
            {
                Vector3 dir = _colliders[i].transform.position - transform.position;
                dir.Normalize();
                if (Physics.Raycast(transform.position, dir, Getradius()))
                {
                    isCanShoot = true;
                    target = dir;
                    break;
                }
            }
            else
            {
                isCanShoot = false;
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
    public override void Die()
    {
        _agent.ResetPath();
        currentstate = null;
        base.Die();
        BotManager.Instance.SetBotOnMap();
    }
    public override void Attack(PoolType type, Vector3 dir)
    {
        base.Attack(type, dir);

        target = Vector3.zero;
    }

    public override void OnDeSpawn()
    {
        base.OnDeSpawn();
    }


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
        if (timeAttack > 2f)
        {
            return true;
        }
        return false;

    }
    public void SetTimeAttack()
    {
        timeAttack = 0;
    }

    public void SetPos(Vector3 newvt)
    {
        _agent.Warp(newvt);
    }

}

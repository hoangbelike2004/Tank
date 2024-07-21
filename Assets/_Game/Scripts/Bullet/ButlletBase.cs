using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButlletBase : GameUnit
{
    [SerializeField] protected float timeDespawn,timer;
    [SerializeField] protected float speed;
    [SerializeField] protected GameObject trailRenderer;
    private int dame;
    private Vector3 dir;
    private Character _current;
    


    private void Start()
    {
        //TF.Rotate(0, 90, 0);
        OnInit();
    }
    public virtual void Excute()
    {
        StartCoroutine(CoroutineUpdate());
    }
    public IEnumerator CoroutineUpdate()
    {
        while (true)
        {
            timeDespawn += Time.deltaTime;
            Move();
            if(timeDespawn > timer)
            {
                OnDespawn();
            }
            yield return null;
        }
    }


    public virtual void Move()
    {
        TF.position += dir * speed * Time.deltaTime;
    }

    public virtual void OnInit()
    {

    }

    public virtual void OnDespawn()
    {
        DeActiveTrail();
        SimplePool.Despawn(this);
        Particel_ExplosionFire particel = SimplePool.Spawn<Particel_ExplosionFire>(PoolType.partical_explosionFire, transform.position, transform.rotation);
        particel.gameObject.SetActive(true);
        particel.PlayPar();
        timeDespawn = 0;
    }
    public void DeActiveTrail()
    {
        trailRenderer.SetActive(false);
    }
    public void ActiveTrail()
    {
        trailRenderer.SetActive(true);
    }
    //getter and setter

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
    public void SetTimeDespawnBullet(float timer)
    {
        this.timer = timer;
    }
    public void SetDame(int dame)
    {
        this.dame = dame;
    }
    public void SetDir(Vector3 dir)
    {
        this.dir = dir; 
    }

    public float GetSpeed()
    {
        return speed;
    }

    public int GetDame()
    {
        return dame;
    }
    public Vector3 GetDir()
    {
        return  dir;
    }
    public void SetParent(Character chr)
    {
        _current = chr;
    }
    public Character GetParent()
    {
        return _current;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Character>() != null)
        {
            Character target = CacheObject.GetCharacter(other.GetComponent<Character>());
            if(target != _current)
            {
                target.GetComponent<Character>().OnHit(_current.GetDame());
                OnDespawn();
            }
            
        }else if (other.CompareTag("Wall"))
        {
            OnDespawn();
        }

    }
    
}

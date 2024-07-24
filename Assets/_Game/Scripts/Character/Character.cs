using UnityEngine;

public class Character : MonoBehaviour
{

    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected GameObject upperBody;
    [SerializeField] protected Animator anim;
    [SerializeField] protected Transform shotPos;
    [SerializeField] protected float speed, time_value_attack = 2f, timeDespawnBullet;//toc do di chuyen va thoi gian nap dan
    [SerializeField] protected int hp, dame, defense, fireRace, hptmp;//mau,sat thuong,phong thu, toc do dan
    [SerializeField] protected float attackRange;
    [SerializeField] protected Bullet _prefab;
    [SerializeField] protected ParticleSystem _Mullzepartical;
    [SerializeField] protected GameObject steamobject;
    [SerializeField] protected MeshRenderer _meshUpBody, _meshLowerBody, _meshBarrel;


    public Vector3 dirMove, dirAttack;
    public bool isDie, isAttack, isPlay;
    public string currentAnim;
    public DataSkin dataskins;
    public ItemSkin itemskin;

    private Quaternion initialAotationAngle;
    private void Awake()
    {
        initialAotationAngle = Quaternion.Euler(0, 0, 0);
        hptmp = hp;
    }
    void Start()
    {
        OnInit();
    }
    public virtual void OnInit()
    {
        _Mullzepartical.Stop();
        isDie = false;
        isPlay = false;
        hp = hptmp;
        transform.rotation = initialAotationAngle;
        upperBody.transform.rotation = initialAotationAngle;

    }

    public virtual void OnDeSpawn()
    {
        gameObject.SetActive(false);
    }

    public virtual void OnHit(int dame)
    {
        hp -= (dame - defense);
        if (hp <= 0)
        {
            Die();
            return;
        }

    }

    public virtual void Move(Vector3 dir)
    {
        rb.velocity = dir * speed;
    }

    public virtual void ChangeSkin(TypeSkin typeSkin)
    {
        itemskin = dataskins.GetItemSkin(typeSkin);
        _meshUpBody.material = itemskin._materialSkin;
        _meshLowerBody.material = itemskin._materialSkin;
        _meshBarrel.material = itemskin._materialSkin;
        _prefab = itemskin._bullet;
        hp = itemskin.blood_Index;
        speed = itemskin.speed_Index;
        dame = itemskin.dameAttack_Index;
        defense = itemskin.defense_Index;
        fireRace = itemskin.fireRace_Index;


    }

    public virtual void Attack(PoolType type, Vector3 dir)
    {

        _Mullzepartical.gameObject.SetActive(true);
        _Mullzepartical.Play();//effect ban
        Bullet bullet = SimplePool.Spawn<Bullet>(type, shotPos.position, upperBody.transform.rotation);
        bullet.gameObject.SetActive(true);
        bullet.SetSpeed(fireRace);
        bullet.SetDame(dame);
        bullet.OnInit();
        bullet.SetTimeDespawnBullet(timeDespawnBullet);//thoi gian de huy vien dan
        bullet.SetDir(dir);//set dir cho dan
        bullet.SetParent(this);//cho vien dan biet ai la nguoi spawn ra no
        bullet.Excute();//chay ienumtor update transform
        bullet.ActiveTrail();//active trail

    }

    public virtual void Die()
    {
        isDie = true;
        Invoke(nameof(OnDeSpawn), 3f);
    }

    public virtual void ChangeAnim(string nameanim)
    {
        if (currentAnim != nameanim)
        {
            anim.ResetTrigger(nameanim);
            currentAnim = nameanim;
            anim.SetTrigger(currentAnim);
        }

    }

    public void DeAcitveSteam()
    {
        steamobject.SetActive(false);
    }

    public void ActiveSteam()
    {
        steamobject.SetActive(true);
    }
    //Getter and Setter
    public virtual int GetDame()
    {
        return dame;
    }
    public float GetAttackRange()
    {
        return attackRange;
    }

    public void SetAttackRange(float range)
    {
        if (range < 1.5f)
        {
            attackRange += range;
        }
    }
    public void SetAttacking()
    {
        isAttack = false;
    }

    public TypeSkin RandomSKin()
    {
        return (TypeSkin)Random.Range(0, dataskins.skins.Count);
    }
    public virtual void SetPlaying(bool isplay)
    {
        this.isPlay = isplay;
        Debug.Log("Param: " + isplay);
        Debug.Log(this.isPlay);
    }


    protected virtual void OnEnable()
    {
        GameAction.GamePlayAction += SetPlaying;
    }

    protected virtual void OnDisable()
    {
        GameAction.GamePlayAction -= SetPlaying;
    }


}




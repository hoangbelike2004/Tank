using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private VariableJoystick joystick_Move;
    [SerializeField] private VariableJoystick joystick_Attack;
    [SerializeField] private float  timeStart, timeEnd;
    [SerializeField] private GameObject joystickobject;
    [SerializeField] private Canvas_Playing _canvasPlaying;
    private Touch[] touches;
    private Touch touch;
    private bool isTouchStart;
    private ItemSkin _itemskin;

    public void wake()
    {
        ChangeSkin(TypeSkin.tank_blue);
    }
    public override void OnInit()
    {
        base.OnInit();
        _itemskin = itemskin;//luu lai itemskin
        if (_canvasPlaying != null)
        {
            _canvasPlaying.UpdateBloodBar(hp, hptmp);
        }
    }
    void Update()
    {
        if (isDie) return;
            Debug.Log(1);
            touches = Input.touches;
            dirMove = new Vector3(joystick_Move.Horizontal, 0, joystick_Move.Vertical);
            if (joystickobject.activeSelf == true)
            {
                dirAttack = new Vector3(joystick_Attack.Horizontal, 0, joystick_Attack.Vertical);
            }
            Move(dirMove);
            CheckTouch();
       

    }
    public override void Attack(PoolType t, Vector3 dir)
    {
        ChangeAnim(Constant.ANIM_ATTACK);
        base.Attack(t, dir);
        isAttack = true;
    }
    public override void ChangeSkin(TypeSkin typeSkin)
    {
        base.ChangeSkin(typeSkin);
    }

    public override void OnHit(int dame)
    {
        base.OnHit(dame);
        if (_canvasPlaying != null)
        {
            _canvasPlaying.UpdateBloodBar(hp, hptmp);
        }

    }

    public override void Die()
    {
        base.Die();
        GameManager.Instance.GameLose();
    }
    //Getter and Setter
    public float GetValueTimeAttack()
    {
        return time_value_attack;
    }
  
    private void LateUpdate()
    {
        if (!isPlay) return;
        upperBody.transform.forward = dirAttack;
    }

    public override void Move(Vector3 dir)
    {
        base.Move(dir);
        if (rb.velocity.magnitude > 0.1f)
        {
            //if (!isAttack)
            //{
                ChangeAnim(Constant.ANIM_RUN);
                ActiveSteam();
            //}

            transform.forward = dirMove;
        }
        else if (rb.velocity.magnitude < 0.1f)
        {
            DeAcitveSteam();
            if (!isAttack)
            {
                ChangeAnim(Constant.ANIM_IDLE);
            }

        }
    }
    public void CheckTouch()//kiem tra xem minh co cham vao nua man hinh ben phai(nua ban minh cho phep attack)
    {

        if (Input.touchCount > 0 && touches != null)
        {
            for(int i = 0; i < Input.touchCount; i++)
            {
                if (touches[i].position.x > Screen.width / 2)
                {
                    touch = touches[i];
                    break;
                }
            }
            if (touch.position.x > Screen.width / 2)
            {

                if (touch.phase == TouchPhase.Began)
                {
                        isTouchStart = true;
                }
                if ((touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)&& isTouchStart)
                {
                    timeEnd += Time.deltaTime;
                    if (_canvasPlaying != null)
                    {
                        _canvasPlaying.UpdateEnergyBar(timeEnd, time_value_attack);
                    }
                }
                touch = Input.GetTouch(Input.touchCount - 1);
                if (touch.phase == TouchPhase.Ended && touch.position.x > Screen.width / 2)
                {
                    if (timeEnd > time_value_attack && isTouchStart)
                    {
                        dirAttack.Normalize();
                        Attack(PoolType.bullet_Blue, dirAttack);
                    }
                    isTouchStart = false;
                    timeEnd = 0;
                    if(_canvasPlaying != null)
                    {
                        _canvasPlaying.UpdateEnergyBar(timeEnd, time_value_attack);
                    }
                    
                }
            }
            else
            {
                isTouchStart = false;
                timeEnd = 0;
                if (_canvasPlaying != null)
                {
                    _canvasPlaying.UpdateEnergyBar(timeEnd, time_value_attack);
                }
            }
        }
    }
   
    public void SetCanvasPlaying(Canvas_Playing canvas)
    {
        _canvasPlaying = canvas;
    }

    public void Select(ItemSkin item)
    {
        _itemskin = item;
        ChangeSkin(item._typeSkin);
    }

    public void NotSelect()
    {
        ChangeSkin(_itemskin._typeSkin);
    }


    private void OnEnable()
    {
        GameAction.TryOnOutfitsAciton += ChangeSkin;
        GameAction.NotSelectSkinAction += NotSelect;
        GameAction.SelectSkinAction += Select;
    }

    private void OnDisable()
    {
        GameAction.TryOnOutfitsAciton -= ChangeSkin;
        GameAction.NotSelectSkinAction -= NotSelect;
        GameAction.SelectSkinAction += Select;
    }
}

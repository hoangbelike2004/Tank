using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartrolState : IState
{
    float time, timer;
    Vector3 next_Pos;
    public void OnEnter(Bot bot)
    {
        bot.ActiveSteam();
        bot.ChangeAnim(Constant.ANIM_RUN);
        next_Pos = bot.transform.position;
        time = 0;
        timer = Random.Range(bot.randomRunstart, bot.randomRunend);
    }

    public void OnExcute(Bot bot)
    {
        time += Time.deltaTime;
        if (time <= timer)
        {
            if (Vector3.Distance( next_Pos, bot.transform.position) < 0.5f)
            {
               
                next_Pos = RandomTarget.Instance.R_point_Get(bot._playerDirection(),bot.Getradius());
                bot.Move(next_Pos);
                next_Pos.y = bot.transform.position.y;
            }

            if(bot.target != Vector3.zero && bot.IsAttack())
            {
                bot.isAttack = true;
                bot.ChangeAnim(Constant.ANIM_RUN);
               
            }

        }
        else
        {
            bot.ChangState(new IdleState());
        }
    }

    public void OnExit(Bot bot)
    {

    }
}

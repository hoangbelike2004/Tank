using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    float time, timer;
    public void OnEnter(Bot bot)
    {
        bot.DeAcitveSteam();
        bot.ChangeAnim(Constant.ANIM_IDLE);
        time = 0;
        timer = Random.Range(bot.randomIdlestart,bot.randomIdleend);
    }

    public void OnExcute(Bot bot)
    {
        time += Time.deltaTime;
        if(time > timer)
        {
            bot.ChangState(new PartrolState());
        }
    }

    public void OnExit(Bot bot)
    {
        
    }
}

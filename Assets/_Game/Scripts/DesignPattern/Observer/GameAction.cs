using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class GameAction
{
    //action camera
    public static Action<TypeCamera> ChangeSkinAndChangeCameraAction;
    public static Action<TypeCamera> GoHomeAndChangeCameraAction;


    //action Game Lose and Win
    public static Action GameLoseAction;
    public static Action GameWinAction;

    //action change skin
    public static Action<TypeSkin> TryOnOutfitsAciton;
    public static Action NotSelectSkinAction;
    public static Action<ItemSkin> SelectSkinAction;

    //Action Game Play
    public static UnityAction<bool> GamePlayAction;
    public static Action GamePlayInstanceBotAction;

}

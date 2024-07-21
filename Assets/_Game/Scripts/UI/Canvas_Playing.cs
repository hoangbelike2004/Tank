using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Playing : UiCanvas
{
    [SerializeField] private Image energyBar;
    [SerializeField] private Image bloodBar;



    public void UpdateEnergyBar(float time,float timer)
    {
        energyBar.fillAmount = time/timer;
    }

    public void UpdateBloodBar(float hp,float hptmp)
    {
        bloodBar.fillAmount = hp / hptmp;
    }
}

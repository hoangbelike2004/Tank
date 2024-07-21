using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeSkin { 
    tank_blue = 0,
    tank_green = 1,
    tank_red = 2,
    tank_yellow = 3,
}
[CreateAssetMenu(menuName = "Data Skin")]
public class DataSkin : ScriptableObject
{
    public List<ItemSkin> skins = new List<ItemSkin>();

    public ItemSkin GetItemSkin(TypeSkin type)
    {
        return skins[(int)type];
    }
}

[System.Serializable]
public class ItemSkin
{
    public Material _materialSkin;
    public string _name;
    public int _price;
    public Bullet _bullet;
    public PoolType poolType;
    public TypeSkin _typeSkin;
    public int speed_Index;//chi so toc do di chuyen
    public int defense_Index;//chi so phong thu(giap)
    public int dameAttack_Index;//chi so tan cong(dame)
    public int blood_Index;// chi so hp
    public int fireRace_Index;// chi so toc do ban
    public StateSkin _stateSkin;
}
public enum StateSkin
{
    not_yel_bought = 0,
    bought = 1,
    selected = 2
}

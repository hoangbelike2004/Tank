using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolControler : MonoBehaviour
{
    [SerializeField] PoolAmount[] poolamounts;
    private void Awake()
    {
        for (int i = 0; i < poolamounts.Length; i++)
        {
            SimplePool.PreLoad(poolamounts[i].prefab, poolamounts[i].amount, poolamounts[i].parent);
        }
    }
}
[System.Serializable]

public class PoolAmount
{
    public GameUnit prefab;
    public Transform parent;
    public int amount;
}
public enum PoolType
{
    bullet_Blue = 0,
    bullet_Green = 1,
    bullet_Red = 2,
    bullet_Yellow = 3,
    partical_explosionFire = 4
}
;
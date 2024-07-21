using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SimplePool
{
    private static Dictionary<PoolType,Pool> poolInstance = new Dictionary<PoolType,Pool>();

    //khoi tao pool
    public static void PreLoad(GameUnit prefab,int amount,Transform parent)
    {
        if(prefab == null)
        {
            Debug.LogError("prefab is empty");
            return;
        }

        if(!poolInstance.ContainsKey(prefab.poolType) || poolInstance[prefab.poolType] == null)
        {
            Pool P = new Pool();
            P.PreLoad(prefab, amount, parent);
            poolInstance[prefab.poolType] = P;
        }
    }

    //Lay phan tu
    public static T Spawn<T>(PoolType poolType,Vector3 pos, Quaternion rot) where T : GameUnit
    {
        if (!poolInstance.ContainsKey(poolType))
        {
            Debug.LogError($"{poolType} IS NOT PRELOAD");
            return null;
        }
        return poolInstance[poolType].Spawn(pos, rot) as T;
    }
    //tra phan tu
    public static void Despawn(GameUnit gameUnit)
    {
        if (!poolInstance.ContainsKey(gameUnit.poolType))
        {
            Debug.LogError($"{gameUnit.poolType} IS NOT PRELOAD");
            return;
        }
        poolInstance[gameUnit.poolType].Despawn(gameUnit);
    }

    //thu thap 1 phan tu
    public static void Collect(PoolType poolType)
    {
        if (!poolInstance.ContainsKey(poolType))
        {
            Debug.LogError($"{poolType} IS NOT PRELOAD");
        }
        poolInstance[poolType].Collect();
    }

    //thu thap tat ca
    public static void CollectAll()
    {
        foreach(var item in poolInstance.Values)
        {
            item.Collect();
        }
    }

    //destroy 1 thang pool ko dung
    public static void Release(PoolType poolType)
    {
        if (!poolInstance.ContainsKey(poolType))
        {
            Debug.LogError($"{poolType} IS NOT PRELOAD");
        }
        poolInstance[poolType].Rellease();
    }

    //destroy all thang pool ko dung
    public static void ReleaseAll()
    {
        foreach (var item in poolInstance.Values)
        {
            item.Rellease();
        }
    }

}

public class Pool
{
    Transform parent;
    GameUnit prefab;

    //list chua các Gameunit ðang o trong pool
    Queue<GameUnit> inactives = new Queue<GameUnit>();
    //list chua các Gameunit ðang duoc su dung
    List<GameUnit> actives = new List<GameUnit>();

    //khoi tao pool
    public void PreLoad(GameUnit prefab,int amount,Transform parent)
    {
        this.prefab = prefab;
        this.parent = parent;

        for(int i = 0;i < amount; i++)
        {
            Despawn(Spawn(Vector3.zero,Quaternion.identity));
        }
    }


   //lay phan tu ra tu pool
    public GameUnit Spawn(Vector3 pos, Quaternion rot)
    {
        GameUnit unit;
        if(inactives.Count <= 0)
        {
            unit = GameObject.Instantiate(prefab, parent);
        }
        else
        {
            unit = inactives.Dequeue();//lay mot phan tu ra khoi queue
        }
        
        unit.TF.SetPositionAndRotation(pos, rot);
        actives.Add(unit);

        return unit;
    }

    //tra phan tu vao trong pool
    public void Despawn(GameUnit unit)
    {
        if (unit != null && unit.gameObject.activeSelf)
        {
            actives.Remove(unit);
            inactives.Enqueue(unit);//add 1 phan tu vào trong queue
            unit.gameObject.SetActive(false);
        }
    }

    //thu thap all phan tu dang su dung vao trong pool(xoa cac thang dang su dung cho vao pool)
    public void Collect()
    {
        while (actives.Count > 0)
        {
            Despawn(actives[0]);
        }
    }


    //destroy all cac phan tu
    public void Rellease()
    {
        Collect();
        while (inactives.Count > 0)
        {
            GameObject.Destroy(inactives.Dequeue().gameObject);
        }
        inactives.Clear();
    }
}

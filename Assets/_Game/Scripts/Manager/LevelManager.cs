using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private List<Map> maps = new List<Map>();
    public int indexLevel = 0;

    public Map GetMap()
    {
        return maps[indexLevel];
    }
    public Vector3 GetPosStartOfPlayer()
    {
        return maps[indexLevel].GetPosStartOfPlayer();
    }
    public void SetLevel()
    {
        indexLevel++;
    }

    public void LoadLevel()
    {
        maps[indexLevel - 1].gameObject.SetActive(false);
        Instantiate(maps[indexLevel],new Vector3(0,0,0),Quaternion.identity);
    }


}

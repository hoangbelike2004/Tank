using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotManager : Singleton<BotManager>
{
    [SerializeField] private Bot _BotPrefab;
    [SerializeField] private List<Bot> _Bots = new List<Bot>();
    [SerializeField] private int amountBotOnMap, amountBotMax, amountBotTmp;
    [SerializeField] private bool isPlay;


    private void Start()
    {
        amountBotTmp = 0;
        amountBotOnMap = amountBotMax;
    }
    public void OnInit()
    {
        for (int i = 0; i < LevelManager.Instance.GetAmountPosMap(); i++)
        {
            Bot prefab = Instantiate(_BotPrefab);
            prefab.OnInit();
            _Bots.Add(prefab);
        }
    }
    public void StartCoroutinueBotManager()
    {
        StartCoroutine(ManagerBot());
    }

    public void StopCoroutinueBotManager()
    {
        StopCoroutine(ManagerBot());
    }
    IEnumerator ManagerBot()
    {
        int a = 0;
        while (true)
        {
            a++;
            if (a < _Bots.Count)
            {
                if (!_Bots[a].gameObject.activeSelf)
                {
                    _Bots[a].gameObject.SetActive(true);
                    _Bots[a].OnInit();
                    _Bots[a].transform.position = LevelManager.Instance.GetPosStartOfBot();
                    amountBotTmp++;
                }

                if (amountBotTmp >= amountBotMax - _Bots.Count)
                {
                    StopCoroutinueBotManager();
                }
            }
            else
            {
                a = 0;
            }

            yield return null;
        }
    }

    //Deactive nhung con bot khi khong dung den
    public void DeActiveBotWhenNotUsed()
    {
        for (int i = 0; i < _Bots.Count; i++)
        {
            _Bots[i].OnDeSpawn();
        }
    }

    //tang hoac giam so bot dua vao level
    public void SetAmountBotWhenNextLevel()
    {
        int amount = 0;
        if(_Bots.Count == 0)
        {
            OnInit();
        }
        //khong lay truong hop list bot < hay == GetAmountPosMap vi no chi active amount bot theo GetAmountPosMap
        if (_Bots.Count < LevelManager.Instance.GetAmountPosMap())
        {
           amount = LevelManager.Instance.GetAmountPosMap() - _Bots.Count;
            for(int i = 0; i < amount; i++)
            {
                Bot prefab = Instantiate(_BotPrefab);
                prefab.OnInit();
                _Bots.Add(prefab);
            }
        }
        SetPosForBot();
    }


    //Set position for bots and active bot
    public void SetPosForBot()
    {
        for (int i = 0; i < _Bots.Count; i++)
        {
            if (!_Bots[i].gameObject.activeSelf)
            {
                _Bots[i].gameObject.SetActive(true);
            }
            
            _Bots[i].SetPos(LevelManager.Instance.GetPosStartOfBot());
            
        }
    }

    public int GetAmountBotOnMap()
    {
        return amountBotOnMap;
    }

    //khi 1 con bot die thi se set lai sl bot tren map
    public void SetBotOnMap()
    {
        amountBotOnMap -= 1;
        if (amountBotOnMap <= 0)
        {
            GameAction.GameWinAction?.Invoke();
        }
    }
}

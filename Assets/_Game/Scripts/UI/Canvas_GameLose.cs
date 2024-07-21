using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_GameLose : UiCanvas
{
    [SerializeField] private TextMeshProUGUI txtTop;
    [SerializeField] private TextMeshProUGUI txtScore;
    [SerializeField] private TextMeshProUGUI txtTimer;
    [SerializeField] private Button btnHome;
    [SerializeField] private Button btnReLoad;
    [SerializeField] private int timer;

    private void Start()
    {
        btnHome.onClick.AddListener(GoBackHome);
        btnReLoad.onClick.AddListener(ReLoad);
    }

    public void GoBackHome()
    {
        GameManager.Instance.SetGameState(GameState.gohome);
    }

    public void ReLoad()
    {

    }

    IEnumerator CountDownTime()
    {
        for(int i = timer; i >= 0; i--)
        {
            txtTimer.text = i.ToString();
            if(i == 0)
            {
                GoBackHome();
            }
            yield return new WaitForSeconds(1f);
            
        }
    }
    void StartCoroutinueCoutDownTime()
    {
        StartCoroutine(CountDownTime());
    }

    private void OnEnable()
    {
        GameAction.GameLoseAction += StartCoroutinueCoutDownTime;
    }

    private void OnDisable()
    {
        GameAction.GameLoseAction -= StartCoroutinueCoutDownTime;
    }
}

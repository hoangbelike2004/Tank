using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_GameWin : UiCanvas
{
    [SerializeField] private TextMeshProUGUI txtTop;
    [SerializeField] private TextMeshProUGUI txtScore;
    [SerializeField] private Button btnHome;
    [SerializeField] private Button btnContinue;

    private void Start()
    {
        btnContinue.onClick.AddListener(Continue);
        btnHome.onClick.AddListener(GoBackHome);
    }


    public void GoBackHome()
    {
        GameManager.Instance.SetGameState(GameState.gohome);
    }

    public void Continue()
    {

    }
}

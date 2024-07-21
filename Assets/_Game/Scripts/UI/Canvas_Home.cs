using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Home : UiCanvas
{
    [SerializeField] private Button btnSkin;
    [SerializeField] private Button btnSetting;
    [SerializeField] private Button btnPlay;
    [SerializeField] private TextMeshProUGUI txtscore;

    private void Start()
    {
        btnSetting.onClick.AddListener(SettingGame);
        btnSkin.onClick.AddListener(ChangeSkin);
        btnPlay.onClick.AddListener(PLayGame);
    }

    public void PLayGame()
    {
        GameAction.ChangeSkinAndChangeCameraAction?.Invoke(TypeCamera.cameraMain);
        GameManager.Instance.SetGameState(GameState.game_Play);
        UIManager.Instance.CloseUI<Canvas_Home>(0f);
        UIManager.Instance.OpenUI<Canvas_Playing>();
    }

    public void SettingGame()
    {
        
    }

    public void ChangeSkin()
    {
        GameAction.ChangeSkinAndChangeCameraAction?.Invoke(TypeCamera.cameraSkin);
        UIManager.Instance.CloseUI<Canvas_Home>(0f);
        UIManager.Instance.OpenUI<Canvas_ChangeSkin>();
        UIManager.Instance.GetUI<Canvas_ChangeSkin>().SetValue();
        GameManager.Instance.SetGameState(GameState.changskin);
    }

}

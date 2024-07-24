using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Player _player;
    [SerializeField] private RotationGameObject _rotationObject;
    [SerializeField] private int _score;
    public GameState gameState;
    public Transform PosChangSkin;
    public void CheckGameState()
    {
        if(GameState.game_win == gameState)
        {
            GameWin();
        }else if (GameState.game_Lose == gameState)
        {
            GameLose();
        }
        else if (GameState.game_Play == gameState)
        {
            GamePlay();
            _rotationObject.enabled = false;
        }
        else if (GameState.game_Stop == gameState)
        {
            GameStop();
        }
        else if (GameState.changskin == gameState)
        {
            ChangSkin();
            _rotationObject.enabled = true;
        }
        else if (GameState.gohome == gameState)
        {
            Home();
            _rotationObject.enabled = false;
        }

        if (gameState != GameState.game_Play)//khi game trong trang thai khong phai la gameplay thi player ko use joystick
        {
            UIManager.Instance.DeActiveJoyStick();
            BotManager.Instance.StopCoroutinueBotManager();
            BotManager.Instance.DeActiveBotWhenNotUsed();
            GameAction.GamePlayAction?.Invoke(false);
        }



    }
    public void GameWin()
    {
        UIManager.Instance.CloseUI<Canvas_Playing>(0f);
        UIManager.Instance.OpenUI<Canvas_GameLose>();
    }

    public void GameLose()
    {
        UIManager.Instance.CloseUI<Canvas_Playing>(0f);
        UIManager.Instance.OpenUI<Canvas_GameLose>();
        GameAction.GameLoseAction?.Invoke();
    }

    public void GamePlay()
    {
        BotManager.Instance.SetAmountBotWhenNextLevel();
        _player.transform.position = LevelManager.Instance.GetPosStartOfPlayer();
        _player.SetCanvasPlaying(UIManager.Instance.GetUI<Canvas_Playing>());
        BotManager.Instance.StartCoroutinueBotManager();
        UIManager.Instance.ActiveJoyStick();
        GameAction.GamePlayAction?.Invoke(true);
    }

    public void GameStop()
    {

    }
    public void Home()
    {
        UIManager.Instance.CloseAllUI();
        UIManager.Instance.OpenUI<Canvas_Home>();
        GameAction.GoHomeAndChangeCameraAction?.Invoke(TypeCamera.cameraSkin);
        if (!_player.gameObject.activeSelf)
        {
            _player.gameObject.SetActive(true);
        }
        _player.OnInit();
        
    }
    public void ChangSkin()
    {
        _player.transform.position = PosChangSkin.position;
        _player.OnInit();
    }

    //getter and setter
    public GameState GetGameState()
    {
        return gameState;
    }
    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState;
        CheckGameState();
    }
    public void SetScore(int price)
    {
        _score -= price;
    }
    public int GetScore()
    {
        return _score;
    }
}
public enum GameState
{
    game_win = 0,
    game_Lose = 1,
    game_Play = 2,
    game_Stop = 3,
    gohome = 4,
    changskin = 5
}

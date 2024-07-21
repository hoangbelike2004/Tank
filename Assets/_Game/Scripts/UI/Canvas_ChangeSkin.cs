using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_ChangeSkin : UiCanvas
{
    [SerializeField] private Button btnLeft;
    [SerializeField] private Button btnRight;
    [SerializeField] private Button btnExit;
    [SerializeField] private Button btnSelect;
    [SerializeField] private Button btnBuy;
    
    //txt
    [SerializeField] private TextMeshProUGUI _txtName;
    [SerializeField] private TextMeshProUGUI _txtPrice;

    //Index of tank max
    [SerializeField] private int maxHp, maxDeferse, maxDame, maxFireRace, maxSpeed;

    [SerializeField] private Image imgSpeedIndex;
    [SerializeField] private Image imgHpIndex;
    [SerializeField] private Image imgDeferseIndex;
    [SerializeField] private Image imgDameIndex;
    [SerializeField] private Image imgFireRaceIndex;

    [SerializeField] private DataSkin dataskin;
    public int value;
    public ItemSkin itemSkin;

    private void Start()
    {
        itemSkin = dataskin.skins[value];
        btnLeft.onClick.AddListener(NextLeft);
        btnRight.onClick.AddListener(NextRight);
        btnExit.onClick.AddListener(ExitShop);
        btnSelect.onClick.AddListener(Select);
        btnBuy.onClick.AddListener(Buy);
        UpdateIndex(itemSkin.blood_Index, itemSkin.speed_Index, itemSkin.defense_Index, itemSkin.dameAttack_Index, itemSkin.fireRace_Index);
        UpdateWhenChange();
        GameAction.TryOnOutfitsAciton?.Invoke(itemSkin._typeSkin);
    }

    public void NextLeft()
    {
        value--;
        if(value < 0)
        {
            value = dataskin.skins.Count -1;
        }
        itemSkin = dataskin.skins[value];
        UpdateIndex(itemSkin.blood_Index,itemSkin.speed_Index,itemSkin.defense_Index,itemSkin.dameAttack_Index,itemSkin.fireRace_Index);
        GameAction.TryOnOutfitsAciton?.Invoke(itemSkin._typeSkin);
        UpdateWhenChange();
    }

    public void NextRight()
    {
        value++;
        if(value >= dataskin.skins.Count)
        {
            value = 0;
        }
        itemSkin = dataskin.skins[value];
        UpdateIndex(itemSkin.blood_Index, itemSkin.speed_Index, itemSkin.defense_Index, itemSkin.dameAttack_Index, itemSkin.fireRace_Index);
        GameAction.TryOnOutfitsAciton?.Invoke(itemSkin._typeSkin);
        UpdateWhenChange();
    }


    public void ExitShop()
    {
        SetStateItem();
        GameAction.NotSelectSkinAction?.Invoke();
        GameManager.Instance.SetGameState(GameState.gohome);
    }

    public void Select()
    {
        itemSkin._stateSkin = StateSkin.selected;
        GameManager.Instance.SetScore(itemSkin._price);
        GameAction.SelectSkinAction?.Invoke(itemSkin);
        UpdateWhenChange();
        SetStateItem();
    }

    public void Buy()
    {
        if(GameManager.Instance.GetScore() < itemSkin._price)
        {
            UIManager.Instance.OpenUI<Canvas_Message>();
        }
        else
        {
            itemSkin._stateSkin = StateSkin.bought;
            UpdateWhenChange();
        }
    }
    public void UpdateIndex(int hp,int speed,int deferse,int dame,int firerace)
    {
        imgHpIndex.fillAmount = (float)hp / (float)maxHp;
        imgFireRaceIndex.fillAmount = (float)firerace / (float)maxFireRace;
        imgSpeedIndex.fillAmount = (float)speed / (float)maxSpeed;
        imgDeferseIndex.fillAmount = (float)deferse/ (float)maxDeferse;
        imgDameIndex.fillAmount = (float)dame/ (float)maxDame;
    }

    //update ten , stateSkin
    public void UpdateWhenChange()
    {
        if(itemSkin._stateSkin == StateSkin.not_yel_bought)
        {
            btnBuy.gameObject.SetActive(true);
            btnSelect.gameObject.SetActive(false);
            _txtPrice.text = itemSkin._price.ToString();
        }
        else if (itemSkin._stateSkin == StateSkin.bought)
        {
            btnBuy.gameObject.SetActive(false);
            btnSelect.gameObject.SetActive(true);
        }
        else
        {
            btnBuy.gameObject.SetActive(false);
            btnSelect.gameObject.SetActive(false);
        }
        _txtName.text = itemSkin._name;
    }
    public void SetStateItem()
    {
        for (int i = 0; i < dataskin.skins.Count; i++)
        {
            if (i != (int)itemSkin._typeSkin && dataskin.skins[i]._stateSkin == StateSkin.selected)
            {
                dataskin.skins[i]._stateSkin = StateSkin.bought;
            }
        }
    }
    public void SetValue()
    {
        value = 0;
        itemSkin = dataskin.skins[value];
        GameAction.TryOnOutfitsAciton?.Invoke(itemSkin._typeSkin);
        UpdateIndex(itemSkin.blood_Index, itemSkin.speed_Index, itemSkin.defense_Index, itemSkin.dameAttack_Index, itemSkin.fireRace_Index);
        UpdateWhenChange();
    }

    
}

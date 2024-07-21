using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Message : UiCanvas
{
    [SerializeField] private Button _btnOk;
    [SerializeField] private TextMeshProUGUI _txtMessage;
    void Start()
    {
        _btnOk.onClick.AddListener(DeActive);
    }

    public void DeActive()
    {
        UIManager.Instance.CloseUI<Canvas_Message>(0f);
    }

 
}

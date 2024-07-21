using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiCanvas : MonoBehaviour
{
    [SerializeField] bool destroyOnClose = false;

    //....FIX TAI THO   
    //private void Awake()
    //{
    //    RectTransform rectTransform = GetComponent<RectTransform>();
    //    float ratio = (float)Screen.width / (float)Screen.height;
    //    if(ratio > 2.1f)
    //    {
    //        Vector2 leftBottom = rectTransform.offsetMin;
    //        Vector2 righttop = rectTransform.offsetMax;

    //        leftBottom.y = 0f;
    //        righttop.y = -100f;
    //        rectTransform.offsetMin = leftBottom;
    //        rectTransform.offsetMax = righttop;
    //    }
    //}
    //goi truoc khi duoc active
    public virtual void SetUp()
    {

    }

    //goi sau khi duoc active
    public virtual void Open()
    {
        gameObject.SetActive(true);
    }

    public virtual void Close(float time)
    {
        Invoke(nameof(CloseDirectly), time);
    }

    public virtual void CloseDirectly()
    {
        if (destroyOnClose)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    Dictionary<System.Type, UiCanvas> canvasActive = new Dictionary<System.Type, UiCanvas>();
    Dictionary<System.Type, UiCanvas> canvasPrefabs = new Dictionary<System.Type, UiCanvas>();
    [SerializeField] Transform parent;
    [SerializeField] GameObject JoystickAttack;
    [SerializeField] GameObject JoystickMove;

    private void Awake()
    {
        UiCanvas[] canvas = Resources.LoadAll<UiCanvas>("UI/");
        for (int i = 0; i < canvas.Length; i++)
        {
            canvasPrefabs.Add(canvas[i].GetType(), canvas[i]);
        }
    }
    private void Start()
    {
        OpenUI<Canvas_Home>();

    }
    //open ui
    public T OpenUI<T>() where T : UiCanvas
    {
        T canvas = GetUI<T>();
        canvas.SetUp();
        canvas.Open();
        return canvas;
    }

    //close ui sau khoang time
    public void CloseUI<T>(float time) where T : UiCanvas
    {
        if (IsOpened<T>())
        {
            canvasActive[typeof(T)].Close(time);
        }
    }

    //close truc tiep
    public void CloseDirectlyUI<T>() where T : UiCanvas
    {
        if (IsOpened<T>())
        {
            canvasActive[typeof(T)].CloseDirectly();
        }
    }


    //kiem tra ui duoc active hay chua
    public bool IsLoaded<T>() where T : UiCanvas
    {
        return canvasActive.ContainsKey(typeof(T)) && canvasActive[typeof(T)] != null;
    }
    //kiem tra xem duoc mo hay chua
    public bool IsOpened<T>() where T : UiCanvas
    {
        return IsLoaded<T>() && canvasActive[typeof(T)].gameObject.activeSelf;
    }
    private T GetUIPrefab<T>() where T : UiCanvas
    {
        return canvasPrefabs[typeof(T)] as T;
    }
    public T GetUI<T>() where T : UiCanvas
    {
        if (!IsLoaded<T>())
        {
            T prefab = GetUIPrefab<T>();//Instantiate()
            T canvas = Instantiate(prefab, parent);
            canvasActive[typeof(T)] = canvas;
        }

        return canvasActive[typeof(T)] as T;
    }

    //dong tat ca cac ui
    public void CloseAllUI()
    {
        foreach (var t in canvasActive)
        {
            if (t.Value != null && t.Value.gameObject.activeSelf)
            {
                t.Value.Close(0);
            }
        }
    }
    public void ActiveJoyStick()
    {
        JoystickAttack.SetActive(true);
        JoystickMove.SetActive(true);
    }

    public void DeActiveJoyStick()
    {
        JoystickAttack.SetActive(false);
        JoystickMove.SetActive(false);
    }
}

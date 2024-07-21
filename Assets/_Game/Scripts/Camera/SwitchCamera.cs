using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    [SerializeField] private GameObject cameraMain;
    [SerializeField] private GameObject cameraChangeSkin;
    [SerializeField] private int valuemanage;
    [SerializeField] SurveilanceCamera surveilanceCamera;
    [SerializeField] Animator anim;

    public void CameraManager(TypeCamera type)
    {
        if(type == TypeCamera.cameraMain)
        {
            Cam_Main();
        }
        else if (type == TypeCamera.cameraSkin)
        {
            Cam_ChangeSkin();
        }
    }

    public void Cam_Main()
    {
        cameraMain.SetActive(true);
        surveilanceCamera.StartCoroutineCame();
        cameraChangeSkin.SetActive(false);

    }
    public void Cam_ChangeSkin()
    {
        surveilanceCamera.StopCoroutineCame();
        cameraMain.SetActive(false);
        cameraChangeSkin.SetActive(true);
    }

    private void OnEnable()
    {
        GameAction.ChangeSkinAndChangeCameraAction += CameraManager;   
        GameAction.GoHomeAndChangeCameraAction += CameraManager;
    }

    private void OnDisable()
    {
        GameAction.ChangeSkinAndChangeCameraAction -= CameraManager;
        GameAction.GoHomeAndChangeCameraAction -= CameraManager;
    }
}
public enum TypeCamera
{
    cameraMain = 0,
    cameraSkin = 1
}

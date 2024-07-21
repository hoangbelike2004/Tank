using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurveilanceCamera : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] float distanY, distanz, valueangle;
    [SerializeField] private float speedCam;

    public Vector3 startPos, followPos;
    public Quaternion startrota, followAngle;

    public void StartCoroutineCame()
    {
        StartCoroutine(Run());
    }
    public void StopCoroutineCame()
    {
        StopCoroutine(Run());
    }
    public IEnumerator Run()
    {
        OnInit();
        while (true)
        {
            StartCoroutine(Follow());
            yield return null;
        }
    }
    void OnInit()
    {
        startPos = transform.position;
        startrota = transform.rotation;
        
    }
    IEnumerator Follow()
    {
        followPos = new Vector3(_player.transform.position.x, distanY, _player.transform.position.z + distanz);
        followAngle = Quaternion.Euler(valueangle, 0, 0);
        transform.position = Vector3.MoveTowards(transform.position, followPos, speedCam * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, followAngle, speedCam * Time.deltaTime);
        yield return null;
    }
}

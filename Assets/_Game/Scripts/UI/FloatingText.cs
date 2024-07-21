using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
     Transform mainCam;
    Transform unit;
    public Transform worldSpaceCam;

     public Vector3 offset;
    IEnumerator Start()
    {
        mainCam = Camera.main.transform;
        unit = transform.parent;

        transform.SetParent(worldSpaceCam);
        while (true)
        {
            transform.rotation = Quaternion.LookRotation(transform.position - mainCam.transform.position);//look at the cam
            transform.position = unit.position + offset;
            yield return null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] List<Transform> startingPositions = new List<Transform>();
    public int indexOfStarting = 0;

    //lay vtri xuat phat cua player
    public Vector3 GetPosStartOfPlayer()
    {
        return startingPositions[0].position;
    }
    public Vector3 GetPosStartOfBot()
    {
        if (indexOfStarting >= startingPositions.Count - 1)
        {
            indexOfStarting = 0;
        }
        indexOfStarting++;
        return startingPositions[indexOfStarting].position;
    }
    public int GetAmountPos()
    {
        return startingPositions.Count - 1;// vi co vi tri chi danh rieng cho player
    }
}

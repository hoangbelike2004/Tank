using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAnimationEvent : MonoBehaviour
{

    [SerializeField] Character _parent;
    public void EndAttack()
    {
        _parent.SetAttacking();
    }
}

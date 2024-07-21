using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void OnEnter(Bot bot);
    void OnExcute(Bot bot);
    void OnExit(Bot bot);
}

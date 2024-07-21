using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particel_ExplosionFire : GameUnit
{
    [SerializeField] ParticleSystem ParticleExplosionFire;
    private void Start()
    {
        var main = GetComponent<ParticleSystem>().main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }
    public void PlayPar()
    {
        ParticleExplosionFire.Play();
    }

    private void OnParticleSystemStopped()
    {
        SimplePool.Despawn(this);
    }
}

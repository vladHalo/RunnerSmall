using System;
using System.Collections;
using Core.Scripts.Tools.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

public class VFXManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _particles;

    public IEnumerator PlayVFX(float z)
    {
        var rand = Random.Range(0, _particles.Length);
        _particles[rand].transform.position = _particles[rand].transform.position.SetZ(z);
        _particles[rand].gameObject.SetActive(true);
        _particles[rand].Play();
        if (AudioManager.instance != null) AudioManager.instance.PlaySoundEffect((SoundType)rand);
        yield return new WaitWhile(() => _particles[rand].IsAlive(true));

        _particles[rand].Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        _particles[rand].gameObject.SetActive(false);
    }

    public IEnumerator PlayVFX(float x, Vector3 position, Action method)
    {
        var rand = Random.Range(0, _particles.Length);
        _particles[rand].transform.position = position.SetX(x);
        _particles[rand].gameObject.SetActive(true);
        _particles[rand].Play();
        if (AudioManager.instance != null) AudioManager.instance.PlaySoundEffect((SoundType)rand);
        yield return new WaitWhile(() => _particles[rand].IsAlive(true));

        _particles[rand].Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        _particles[rand].gameObject.SetActive(false);
        method?.Invoke();
    }
}
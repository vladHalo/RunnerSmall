using System;
using _1Core.Scripts.Patterns.Creational.Singleton;
using Sirenix.Utilities;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] public Button[] _buttons;

    [SerializeField] private AudioSource[] _soundEffects;

    public AudioSource _musicSource;

    private float _startVolumeMusic;

    private void Start()
    {
        _startVolumeMusic = _musicSource.volume;

        _buttons[0].onClick.AddListener(() => SetVolumeMusic(Str.Music, _musicSource));
        _buttons[1].onClick.AddListener(() => SetVolume(Str.Sound, _soundEffects));
        if (ES3.KeyExists(Str.Music))
        {
            _musicSource.volume = ES3.Load<float>(Str.Music);
        }

        if (ES3.KeyExists(Str.Sound))
        {
            var value = ES3.Load<float>(Str.Sound);
            _soundEffects.ForEach(x => x.volume = value);
        }
    }

    public void SetButton(params Button[] buttons)
    {
        _buttons[0] = buttons[0];
        _buttons[1] = buttons[1];
        _buttons[0].onClick.AddListener(() => SetVolumeMusic(Str.Music, _musicSource));
        _buttons[1].onClick.AddListener(() => SetVolume(Str.Sound, _soundEffects));
    }

    public void PlaySoundEffect(SoundType soundType)
    {
        _soundEffects[(int)soundType].Play();
    }

    public void PlaySoundEffect(SoundType soundType, float delay)
    {
        Observable.Timer(TimeSpan.FromSeconds(delay)).Subscribe(_ => _soundEffects[(int)soundType].Play());
    }

    public void PlaySoundEffectTime(SoundType soundType, float time)
    {
        _soundEffects[(int)soundType].Play();
        Observable.Timer(TimeSpan.FromSeconds(time-.1f)).Subscribe(_ => _soundEffects[(int)soundType].Stop());
    }

    private void SetVolume(string musicSound, params AudioSource[] manager)
    {
        float indexVolume = 1;
        if (ES3.KeyExists(musicSound))
            indexVolume = ES3.Load<float>(musicSound);
        indexVolume = indexVolume == 0 ? 1 : 0;
        manager.ForEach(x => x.volume = indexVolume);
        ES3.Save(musicSound, indexVolume);
    }

    private void SetVolumeMusic(string musicSound, params AudioSource[] manager)
    {
        float indexVolume = _startVolumeMusic;
        if (ES3.KeyExists(musicSound))
            indexVolume = ES3.Load<float>(musicSound);
        indexVolume = indexVolume == 0 ? _startVolumeMusic : 0;
        manager.ForEach(x => x.volume = indexVolume);
        ES3.Save(musicSound, indexVolume);
    }
}

public enum SoundType
{
    Money,
    Step,
    Slot,
    Drink
}
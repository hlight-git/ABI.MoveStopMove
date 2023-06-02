using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class SoundManager : Singleton<SoundManager>
{
    [HideInInspector] public AudioSource backgroundSound;
    [HideInInspector] public AudioSource efxSound;


    void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        backgroundSound = gameObject.AddComponent<AudioSource>();
        efxSound = gameObject.AddComponent<AudioSource>();
    }

#region Bg sound
    public void PlayMusicBg(SoundInfor sound)
    {
        if(UserData.Ins.SoundIsOn)
        {
            backgroundSound.clip = sound.Clip;
            backgroundSound.volume = sound.Volume;
            backgroundSound.loop = true;
            backgroundSound.Play();
        }
    }
    public void StopMusicBg()
    {
        backgroundSound.Stop();
    }
    public void SetupMusicBg(bool _b)
    {
        backgroundSound.mute = !_b;
    }

    //volume: 0 ~ 1
    public void UpdateVolumeBg(float volume)
    {
        backgroundSound.volume = volume;
    }
#endregion

#region Efx sound
    public void PlayEfxSound(SoundInfor sound)
    {
        if (UserData.Ins.SoundIsOn)
        {
            efxSound.clip = sound.Clip;
            efxSound.loop = false;
            efxSound.volume = sound.Volume;
            efxSound.PlayOneShot(efxSound.clip);
        }
    }

    public void StopAllEfxSound()
    {
        efxSound.Stop();
    }
#endregion

    public void ChangeSound(SoundInfor sound, float time)
    {
        if (UserData.Ins.SoundIsOn)
        {
            float spacetime = time / 2;

            ChangeVol(.1f, spacetime,
                () =>
                {
                    PlayMusicBg(sound);
                    ChangeVol(1, spacetime, null);
                });
        }
    }

    public void ChangeVol(float vol, float time, UnityAction callBack)
    {
        StartCoroutine(ChangeVolume(vol, time, callBack));
    }

    private IEnumerator ChangeVolume(float vol, float time, UnityAction callBack)
    {
        float stepVol = (vol - backgroundSound.volume) / 10;
        float stepTime = time / 10;

        for (int i = 0; i < 10; i++)
        {
            backgroundSound.volume += stepVol;
            //yield return Cache.GetWFS(stepTime);
            yield return null;
        }

        callBack?.Invoke();
    }

    public void PlayAtPosition(SoundInfor soundInfor, Vector3 position)
    {
        SoundSource soundSource = SimplePool.Spawn<SoundSource>(PoolType.AudioSource, position, Quaternion.identity);
        soundSource.Play(soundInfor.Clip);
    }
}

[Serializable]
public class SoundInfor
{
    public AudioClip Clip;
    [Range(0, 1)]
    public float Volume = 1;
}

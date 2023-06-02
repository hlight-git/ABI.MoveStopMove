using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSource : GameUnit
{
    [SerializeField] AudioSource audioSource;
    public void Play(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
        Invoke(nameof(Despawn), clip.length);
    }
    void Despawn()
    {
        SimplePool.Despawn(this);
    }
}

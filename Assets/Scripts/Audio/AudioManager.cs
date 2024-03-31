using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public PlayAudioEventSO BGMEvent;
    public PlayAudioEventSO FXEvent;
    public PlayAudioEventSO EnemyEvent;
    public AudioSource BGMSource;
    public AudioSource FXSource;
    public AudioSource EnemySource;

    private void OnEnable()
    {
        FXEvent.onEventRaised += OnFXEvent;
        EnemyEvent.onEventRaised += OnEnemyEvent;
    }


    private void OnDisable()
    {
        FXEvent.onEventRaised -= OnFXEvent;
        EnemyEvent.onEventRaised -= OnEnemyEvent;
    }

    private void OnFXEvent(AudioClip clip)
    {
        FXSource.clip = clip;
        FXSource.Play();
    }

    private void OnEnemyEvent(AudioClip clip)
    {
        EnemySource.clip = clip;
        EnemySource.Play();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    public PlayAudioEventSO playAudioEvent;
    public AudioClip hurtClip;
    public AudioClip dieClip;
    
    public void Hurt()
    {
        PlayAudioClip(hurtClip);
    }

    public void Die()
    {
        PlayAudioClip(dieClip);
    }

    private void PlayAudioClip(AudioClip audioClip)
    {
        playAudioEvent.onEventRaised(audioClip);
    }

}

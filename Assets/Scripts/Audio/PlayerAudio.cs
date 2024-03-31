using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public PlayAudioEventSO playAudioEvent;

    public AudioClip hurtClip;
    public AudioClip walkClip;
    public AudioClip dieClip;
    public AudioClip jumpClip;
    public AudioClip healClip;
    public AudioClip crystalClip;
    public AudioClip stopperClip;
    public AudioClip skillClip;
    public AudioClip reverserClip;

    public void Hurt()
    {
        playAudioEvent.onEventRaised(hurtClip);
    }

    public void Walk()
    {
        playAudioEvent.onEventRaised(walkClip);
    }
    
    public void Jump()
    {
        playAudioEvent.onEventRaised(jumpClip);
    }

    public void Die()
    {
        playAudioEvent.onEventRaised(dieClip);
    }

    public void Heal()
    {
        playAudioEvent.onEventRaised(healClip);
    }

    public void Crystal()
    {
        playAudioEvent.onEventRaised(crystalClip);
    }

    public void Stopper()
    {
        playAudioEvent.onEventRaised(stopperClip);
    }

    public void Skill()
    {
        playAudioEvent.onEventRaised(skillClip);
    }

    public void Reverser()
    {
        playAudioEvent.onEventRaised(reverserClip);
    }

    private void PlayAudioClip(AudioClip audioClip)
    {
        playAudioEvent.onEventRaised(audioClip);
    }
}

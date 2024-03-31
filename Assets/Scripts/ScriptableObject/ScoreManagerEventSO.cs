using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(menuName = "Event/ScoreManagerEventSO")]
public class ScoreManagerEventSO : ScriptableObject
{
    public UnityAction<ScoreManager> OnEventRaised;
    public void RaiseEvent(ScoreManager scoreManager)
    {
        OnEventRaised?.Invoke(scoreManager);
    }
}


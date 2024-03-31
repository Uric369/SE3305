using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(menuName = "Event/DragonEventSO")]

public class DragonEventSO : ScriptableObject
{
    public UnityAction<Character> OnEventRaised;
    // Start is called before the first frame update
    public void RaiseEvent(Character character)
    {
        OnEventRaised?.Invoke(character);
    }
}



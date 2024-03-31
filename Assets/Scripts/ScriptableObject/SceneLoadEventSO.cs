using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName ="Event/SceneLoadEventSO")]
public class SceneLoadEventSO : ScriptableObject
{
    public UnityAction<GameSceneSO, Vector3, bool> LoadRequestEvent;

    public void RaiseLoadRequestEvent(GameSceneSO scene, Vector3 position, bool fadeScreen)
    {
        LoadRequestEvent?.Invoke(scene, position, fadeScreen);
    }
}

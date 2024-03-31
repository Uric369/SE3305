using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Initialization : MonoBehaviour
{
    public AssetReference entry;

    private void Awake()
    {
        Addressables.LoadSceneAsync(entry);
    }
}

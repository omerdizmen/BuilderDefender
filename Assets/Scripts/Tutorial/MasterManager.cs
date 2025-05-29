using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObject/dongo")]
public class MasterManager : SingletonScriptableObject<MasterManager>
{
    [SerializeField] private SpeedManager id;

    public static SpeedManager ID => Instance.id;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void FirstInitialize()
    {
        
    }
}

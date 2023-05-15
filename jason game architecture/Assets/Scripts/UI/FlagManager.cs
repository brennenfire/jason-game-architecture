using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FlagManager : MonoBehaviour
{
    [SerializeField] List<GameFlag> allFlags;

    Dictionary<string, GameFlag> flagsByName; 

    public static FlagManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;    
    }

    void Start()
    {
        flagsByName = allFlags.ToDictionary(k => k.name,
                                            v => v);    
    }

    public void Set(string flagName, string value)
    {
        if(flagsByName.TryGetValue(flagName, out var flag) == false) 
        {
            Debug.LogError($"flag not found {flagName}");
            return;
        }

        if(flag is IntGameFlag intGameFlag)
        {
            if (int.TryParse(value, out var intGameValue))
            {
                intGameFlag.Set(intGameValue);
            }
        }
    }
}
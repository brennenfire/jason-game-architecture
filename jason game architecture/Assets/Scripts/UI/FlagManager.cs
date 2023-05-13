using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FlagManager : MonoBehaviour
{
    [SerializeField] List<GameFlag> allFlags;

    public static FlagManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;    
    }

    public void Set(string flagName, string value)
    {
        var flag = allFlags.FirstOrDefault(t => t.name == flagName);
        if(flag == null) 
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
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FlagManager : MonoBehaviour
{
    [SerializeField] GameFlag[] allFlags;

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

    void OnValidate()
    {
        //flagTest = FindObjectsOfType<GameFlag>();
        allFlags = Extensions.GetAllInstances<GameFlag>();
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
        else if(flag is BoolGameFlag boolGameFlag) 
        {
            if (bool.TryParse(value, out var boolGameValue))
            {
                boolGameFlag.Set(boolGameFlag);
            }
        }
        else if (flag is DecimalGameFlag decimalGameFlag)
        {
            if (decimal.TryParse(value, out var decimalGameValue))
            {
                decimalGameFlag.Set(decimalGameValue);
            }
        }
        else if (flag is StringGameFlag stringGameFlag)
        {
            stringGameFlag.Set(value);
        }
    }
}

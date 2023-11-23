using System.Collections.Generic;
using UnityEngine;

internal class StatsManager : MonoBehaviour
{
    public static StatsManager Instance { get; private set; }

    [SerializeField] Stat[] allStats;
    Dictionary<Stat, int> myStats = new Dictionary<Stat, int>();

    void OnValidate()
    {
        allStats = Extensions.GetAllInstances<Stat>();
    }

    void Awake()
    {
        Instance = this;
        foreach (var stat in allStats)
        {
            myStats.Add(stat, stat.DefaultValue);
        }
    }

    public int GetStatValue(Stat stat)
    {
        if (myStats.TryGetValue(stat, out int statValue)) ;
        {
            return statValue;
        }

        return 0;
    }
}
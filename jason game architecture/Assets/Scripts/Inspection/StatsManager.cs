using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class StatsManager : MonoBehaviour
{
    public static StatsManager Instance { get; private set; }

    [SerializeField] Stat[] allStats;
    Dictionary<Stat, StatData> myStats = new Dictionary<Stat, StatData>();
    List<StatData> localStatDatas;

    void OnValidate()
    {
        allStats = Extensions.GetAllInstances<Stat>();
    }

    void Awake()
    {
        Instance = this;
    }

    public int GetStatValue(Stat stat)
    {
        if (myStats.TryGetValue(stat, out var statData)) ;
        {
            return statData.Value;
        }

        return 0;
    }

    public void Bind(List<StatData> statDatas)
    {
        localStatDatas = statDatas;
        foreach (var stat in allStats)
        {
            var data = localStatDatas.FirstOrDefault(t => t.Name == stat.name);
            if(data != null)
            {
                myStats[stat] = data;
            }
            else
            {
                var statData = new StatData { Value = stat.DefaultValue, Name = stat.name };
                localStatDatas.Add(statData);
                myStats[stat] = statData;
            }
        }
    }

    public void Modify(Stat stat, int amount)
    {
        if (myStats.TryGetValue(stat, out var statData))
        {
            statData.Value += amount;
        }
    }
}
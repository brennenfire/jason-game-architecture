using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class StatsManager : MonoBehaviour
{
    public static StatsManager Instance { get; private set; }
    public bool Bound { get; private set; }

    [SerializeField] Stat[] allStats;
    Dictionary<Stat, StatData> myStatDatas = new Dictionary<Stat, StatData>();
    Dictionary<Stat, List<StatMod>> myStatMods = new Dictionary<Stat, List<StatMod>>();
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
        int modValue = 0;
        if (myStatMods.ContainsKey(stat))
        {
            modValue = myStatMods[stat].Sum(t => t.Value);
        }
        if (myStatDatas.TryGetValue(stat, out var statData)) ;
        {
            return statData.Value + modValue;
        }

        return modValue;
    }

    public void Bind(List<StatData> statDatas)
    {
        localStatDatas = statDatas;
        foreach (var stat in allStats)
        {
            var data = localStatDatas.FirstOrDefault(t => t.Name == stat.name);
            if(data != null)
            {
                myStatDatas[stat] = data;
            }
            else
            {
                var statData = new StatData { Value = stat.DefaultValue, Name = stat.name };
                localStatDatas.Add(statData);
                myStatDatas[stat] = statData;
            }
        }

        Bound = true;
    }

    public void Modify(Stat stat, int amount)
    {
        if (myStatDatas.TryGetValue(stat, out var statData))
        {
            statData.Value += amount;
        }
    }

    public IEnumerable<StatData> GetAll()
    {
        return myStatDatas.Values;
    }

    public void AddStatMods(List<StatMod> statMods)
    {
        foreach (var statMod in statMods)
        {
            if (myStatMods.TryGetValue(statMod.StatType, out var existingMods))
            {
                existingMods.Add(statMod);
            }
            else
            {
                myStatMods.Add(statMod.StatType, new List<StatMod>() { statMod });
            }
        }
    }

    public void RemoveStatMods(List<StatMod> statMods)
    {
        foreach (var statMod in statMods)
        {
            if (myStatMods.TryGetValue(statMod.StatType, out var existingMods))
            {
                existingMods.Remove(statMod);
            }
        }
    }
}
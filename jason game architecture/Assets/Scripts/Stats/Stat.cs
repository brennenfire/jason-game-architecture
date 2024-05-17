using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Stat
{
    StatData statDataLocal;
    List<StatMod> mods = new List<StatMod>();
    StatsManager statsManagerLocal;

    public StatType StatType { get; private set; }

    public string Name => StatType.name;

    

    public Stat(StatType statType, StatData data, StatsManager statsManager)
    {
        StatType = statType;
        statDataLocal = data;
        statsManagerLocal = statsManager;
    }

    public float GetValue()
    {
        var totalValue = mods.Sum(t => t.Value) + statDataLocal.Value;
        totalValue = Math.Max(totalValue, StatType.MinimumValue);
        if(StatType.AllowDecimals < 1)
        {
            return Mathf.RoundToInt(totalValue);
        }

        return totalValue;
    }

    public void AddStatMod(StatMod statMod)
    {
        mods.Add(statMod);
    }

    public void RemoveStatMod(StatMod statMod)
    {
        mods.Remove(statMod);
    }

    public void ModifyStatData(float amount)
    {
        var newValue = statDataLocal.Value + amount;
        if (StatType.Maximum != null)
        {
            var maxValue = statsManagerLocal.GetStatValue(StatType.Maximum);
            if (newValue > maxValue)
            {
                newValue = maxValue;
            }
        }
        if(newValue < StatType.MinimumValue) 
        {
            newValue = StatType.MinimumValue;
        }
        statDataLocal.Value = newValue;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Stat
{
    StatData statDataLocal;
    List<StatMod> mods = new List<StatMod>();
    public StatType StatType { get; private set; }

    public string Name => StatType.name;

    

    public Stat(StatType statType, StatData data)
    {
        StatType = statType;
        statDataLocal = data;
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
            var maxValue = StatsManager.Instance.GetStatValue(StatType.Maximum);
            if (newValue > maxValue)
            {
                newValue = maxValue;
            }
        }
        statDataLocal.Value = newValue;
    }
}

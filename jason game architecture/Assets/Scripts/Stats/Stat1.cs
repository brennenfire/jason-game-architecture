using System;
using System.Collections.Generic;
using System.Linq;

public class Stat
{
    StatData statDataLocal;
    List<StatMod> mods = new List<StatMod>();
    StatType statTypeLocal;

    public string Name => statTypeLocal.name;

    public Stat(StatType statType, StatData data)
    {
        statTypeLocal = statType;
        statDataLocal = data;
    }

    public int GetValue()
    {
        var totalValue = mods.Sum(t => t.Value) + statTypeLocal.DefaultValue + statDataLocal.Value;
        return Math.Max(totalValue, statTypeLocal.MinimumValue);
    }

    public void AddStatMod(StatMod statMod)
    {
        mods.Add(statMod);
    }

    public void RemoveStatMod(StatMod statMod)
    {
        mods.Remove(statMod);
    }

    public void ModifyStatData(int amount)
    {
        statDataLocal.Value += amount;
    }
}

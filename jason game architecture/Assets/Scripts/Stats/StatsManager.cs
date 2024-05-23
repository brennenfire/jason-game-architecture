using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public bool Bound { get; private set; }

    [SerializeField] StatType[] allStatTypes;
    Dictionary<StatType, Stat> stats = new Dictionary<StatType, Stat>();
    List<StatData> localStatDatas;

    void OnValidate()
    {
        allStatTypes = Extensions.GetAllInstances<StatType>();
    }

    void Start()
    {
        foreach (var slot in Inventory.Instance.EquipmentSlots)
        {
            slot.Changed += HandleEquipSlotChanged;
            if(slot.Item != null)
            {
                Add(slot.Item);
            }
        }
    }

    void HandleEquipSlotChanged(Item added, Item removed)
    {
        if (added == removed)
        {
            return;
        }

        if (removed)
        {
            Removed(removed);
        }

        if (added)
        {
            Add(added);
        }
    }

    void Removed(Item removed)
    {
        foreach (var statMod in removed.StatMods)
        {
            GetStat(statMod.StatType).RemoveStatMod(statMod);
        }
    }

    void Add(Item added)
    {
        foreach (var statMod in added.StatMods)
        {
            GetStat(statMod.StatType).AddStatMod(statMod);
        }
    }

    public float GetStatValue(StatType statType) => GetStat(statType).GetValue();

    Stat GetStat(StatType statType) => stats[statType];

    public void Bind(List<StatData> statDatas)
    {
        localStatDatas = statDatas;
        foreach (var statType in allStatTypes)
        {
            var data = localStatDatas.FirstOrDefault(t => t.Name == statType.name);
            if (data == null)
            {
                data = new StatData { Value = statType.DefaultValue, Name = statType.name };
                localStatDatas.Add(data);
            }

            stats.Add(statType, new Stat(statType, data, this));
        }

        Bound = true;
    }

    public void Modify(StatType statType, float amount) => GetStat(statType).ModifyStatData(amount);

    public IEnumerable<Stat> GetAll() => stats.Values;

    public void AddStatMods(List<StatMod> statMods)
    {
        foreach (var statMod in statMods)
        {
            GetStat(statMod.StatType).AddStatMod(statMod);
        }
    }

    public void RemoveStatMods(List<StatMod> statMods)
    {
        foreach (var statMod in statMods)
        {
            GetStat(statMod.StatType).RemoveStatMod(statMod);
        }
    }

    internal void Modify(object energyStat, float v)
    {
        throw new NotImplementedException();
    }
}
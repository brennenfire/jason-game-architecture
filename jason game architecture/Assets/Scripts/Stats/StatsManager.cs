using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance { get; private set; }
    public bool Bound { get; private set; }

    [SerializeField] StatType[] allStatTypes;
    Dictionary<StatType, Stat> stats = new Dictionary<StatType, Stat>();
    List<StatData> localStatDatas;

    void OnValidate()
    {
        allStatTypes = Extensions.GetAllInstances<StatType>();
    }

    void Awake()
    {
        Instance = this;
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
                var statData = new StatData { Value = 0, Name = statType.name };
                localStatDatas.Add(statData);
            }

            stats.Add(statType, new Stat(statType, data));
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
}
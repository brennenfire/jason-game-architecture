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

    public int GetStatValue(StatType statType)
    {
        Stat stat = GetStat(statType);
        return stat.GetValue();
        
    }

    Stat GetStat(StatType statType)
    {
        return stats[statType];
    }

    public void Bind(List<StatData> statDatas)
    {
        localStatDatas = statDatas;
        foreach (var statType in allStatTypes)
        {
            var data = localStatDatas.FirstOrDefault(t => t.Name == statType.name);
            if(data == null)
            {
                var statData = new StatData { Value = 0, Name = statType.name };
                localStatDatas.Add(statData);
            }

            stats.Add(statType, new Stat(statType, data));
        }

        Bound = true;
    }

    public void Modify(StatType statType, int amount)
    {
        GetStat(statType).ModifyStatData(amount);
    }

    public IEnumerable<Stat> GetAll()
    {
        return stats.Values;
    }

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
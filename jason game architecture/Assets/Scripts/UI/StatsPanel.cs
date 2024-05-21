using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsPanel : ToggleablePanel
{
    [SerializeField] StatEntry statEntryPrefab;
    [SerializeField] Transform statsPanel;
    List<StatEntry> statEntries = new List<StatEntry>();

    public void Bind(StatsManager statsManager)
    {
        foreach (var statEntry in statEntries)
        {
            Destroy(statEntry.gameObject);
        }

        statEntries.Clear();

        var allStats = statsManager.GetAll();
        foreach (var stat in allStats)
        {
            var statEntry = Instantiate(statEntryPrefab, statsPanel);
            statEntry.Bind(stat);
            statEntries.Add(statEntry); 
        }
    }
}

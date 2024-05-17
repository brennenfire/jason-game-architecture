using System.Collections;
using UnityEngine;

public class StatsPanel : ToggleablePanel
{
    [SerializeField] StatEntry statEntryPrefab;
    [SerializeField] Transform statsPanel;

    IEnumerator Start()
    {
        var statsManager = FindObjectOfType<StatsManager>();
        while(statsManager.Bound)
        {
            yield return new WaitForSeconds(1.0f);
        }
        var allStats = statsManager.GetAll();
        foreach (var stat in allStats) 
        {
            var statEntry = Instantiate(statEntryPrefab, statsPanel);
            statEntry.Bind(stat);
        }
    }
}

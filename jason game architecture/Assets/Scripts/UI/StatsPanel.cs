using System.Collections;
using UnityEngine;

public class StatsPanel : ToggleablePanel
{
    [SerializeField] StatEntry statEntryPrefab;
    [SerializeField] Transform statsPanel;

    IEnumerator Start()
    {
        while(!StatsManager.Instance.Bound)
        {
            yield return new WaitForSeconds(1.0f);
        }
        var allStats = StatsManager.Instance.GetAll();
        foreach (var stat in allStats) 
        {
            var statEntry = Instantiate(statEntryPrefab, statsPanel);
            statEntry.Bind(stat);
        }
    }
}

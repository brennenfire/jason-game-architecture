using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatEntry : MonoBehaviour
{
    [SerializeField] TMP_Text nameLocal;
    [SerializeField] TMP_Text value;

    StatData statDataLocal;
    ToggleablePanel toggleablePanel;

    void Awake()
    {
        toggleablePanel = GetComponentInParent<ToggleablePanel>();
    }

    public void Bind(StatData statData)
    {
        statDataLocal = statData;
        nameLocal.SetText(statDataLocal.Name);
    }

    void Update()
    {
        if(toggleablePanel.IsVisible) 
        {
            value.SetText(statDataLocal.Value.ToString());
        }
    }
}

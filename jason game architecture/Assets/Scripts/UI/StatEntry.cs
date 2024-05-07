using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatEntry : MonoBehaviour
{
    [SerializeField] TMP_Text nameLocal;
    [SerializeField] TMP_Text value;

    Stat statLocal;
    ToggleablePanel toggleablePanel;

    void Awake()
    {
        toggleablePanel = GetComponentInParent<ToggleablePanel>();
    }

    public void Bind(Stat stat)
    {
        statLocal = stat;
        nameLocal.SetText(statLocal.Name);
    }

    void Update()
    {
        if(toggleablePanel.IsVisible) 
        {
            string format = "N" + statLocal.StatType.AllowDecimals;
            value.SetText(statLocal.GetValue().ToString(format));
        }
    }
}

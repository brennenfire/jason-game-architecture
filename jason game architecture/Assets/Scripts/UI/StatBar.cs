using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatBar : MonoBehaviour
{
    [SerializeField] TMP_Text valueText;
    [SerializeField] Image fill;
    [SerializeField] StatType statTypeLocal;
    StatsManager statsManager;

    void Awake()
    {
        statsManager = FindObjectOfType<StatsManager>();    
    }

    void Update()
    {
        if(Player.ActivePlayer == null || Player.ActivePlayer.StatsManager == null) 
        {
            return;
        }

        var value = Player.ActivePlayer.StatsManager.GetStatValue(statTypeLocal);
        valueText.SetText(value.ToString("N" + statTypeLocal.AllowDecimals));

        var percent = value / statsManager.GetStatValue(statTypeLocal.Maximum);
        fill.fillAmount = percent;
    }
}

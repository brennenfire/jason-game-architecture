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

    void Update()
    {
        var value = StatsManager.Instance.GetStatValue(statTypeLocal);
        valueText.SetText(value.ToString("N" + statTypeLocal.AllowDecimals));

        var percent = value / StatsManager.Instance.GetStatValue(statTypeLocal.Maximum);
        fill.fillAmount = percent;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatEntry : MonoBehaviour
{
    [SerializeField] TMP_Text nameLocal;
    [SerializeField] TMP_Text value;

    StatData statDataLocal;

    public void Bind(StatData statData)
    {
        statDataLocal = statData;
    }

    void Update()
    {
        nameLocal.SetText(statDataLocal.Name);
        value.SetText(statDataLocal.Value.ToString());
    }
}

using System.Collections;
using TMPro;
using UnityEngine;

public class InspectionPanel : MonoBehaviour
{
    [SerializeField] TMP_Text hintText;
    [SerializeField] TMP_Text progressText;

    void OnEnable()
    {
        hintText.enabled = false;
        Inspectable.InspectablesInRangeChanged += UpdateHintTextState;
    }

    void OnDisable()
    {
        Inspectable.InspectablesInRangeChanged -= UpdateHintTextState;    
    }

    void UpdateHintTextState(bool enableHint)
    {
        hintText.enabled = enableHint;
    }

    void Update()
    {
        if (InspectionManager.Inspecting)
        {
            progressText.SetText(InspectionManager.InspectionProgress.ToString());
        }
    }
}

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InspectionPanel : MonoBehaviour
{
    [SerializeField] TMP_Text hintText;
    [SerializeField] Image progressBarFilledImage;
    [SerializeField] GameObject progressBar;

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
            progressBarFilledImage.fillAmount = InspectionManager.InspectionProgress;
            progressBar.SetActive(true);
        }
        else if (progressBar.activeSelf)
        {
            progressBar.SetActive(false);
        }
    }
}

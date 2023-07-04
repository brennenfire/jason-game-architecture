using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InspectionPanel : MonoBehaviour
{
    [SerializeField] TMP_Text hintText;
    [SerializeField] TMP_Text completedInspectionText;
    [SerializeField] Image progressBarFilledImage;
    [SerializeField] GameObject progressBar;

    void OnEnable()
    {
        hintText.enabled = false;
        completedInspectionText.enabled = false;
        Inspectable.InspectablesInRangeChanged += UpdateHintTextState;
        Inspectable.AnyInspectionComplete += HandleAnyInspectionComplete;
    }

    void OnDisable()
    {
        Inspectable.InspectablesInRangeChanged -= UpdateHintTextState;
        Inspectable.AnyInspectionComplete -= HandleAnyInspectionComplete;
    }

    void HandleAnyInspectionComplete(Inspectable inspectable, string completedInspectionMessage)
    {
        completedInspectionText.SetText(completedInspectionMessage);
        completedInspectionText.enabled = true;
        float messageTime = completedInspectionMessage.Length / 5;
        messageTime = Mathf.Clamp(messageTime, 3f, 15f);
        StartCoroutine(FadeCompletedText(messageTime));
    }

    IEnumerator FadeCompletedText(float messageTime)
    {
        completedInspectionText.alpha = 1f;
        while (completedInspectionText.alpha > 0f)
        {
            yield return null;
            completedInspectionText.alpha -= Time.deltaTime;
        }

        completedInspectionText.enabled = false;
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

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
        Interactable.InteractablesInRangeChanged += UpdateHintTextState;
        Interactable.AnyInteractionComplete += ShowCompletedInspectionText;
    }

    void OnDisable()
    {
        Interactable.InteractablesInRangeChanged -= UpdateHintTextState;
        Interactable.AnyInteractionComplete -= ShowCompletedInspectionText;
    }

    void ShowCompletedInspectionText(Interactable inspectable, string completedInspectionMessage)
    {
        completedInspectionText.SetText(completedInspectionMessage);
        completedInspectionText.enabled = true;
        float messageTime = completedInspectionMessage.Length / 3;
        messageTime = Mathf.Clamp(messageTime, 3f, 15f);
        StartCoroutine(FadeCompletedText(messageTime));
    }

    IEnumerator FadeCompletedText(float messageTime)
    {
        completedInspectionText.alpha = 1f;
        while (completedInspectionText.alpha > 0f)
        {
            yield return null;
            completedInspectionText.alpha -= Time.deltaTime / messageTime;
        }

        completedInspectionText.enabled = false;
    }

    void UpdateHintTextState(bool enableHint)
    {
        hintText.enabled = enableHint;
    }

    void Update()
    {
        if (InteractionManager.Interaction)
        {
            progressBarFilledImage.fillAmount = InteractionManager.InteractionProgress;
            progressBar.SetActive(true);
        }
        else if (progressBar.activeSelf)
        {
            progressBar.SetActive(false);
        }
    }
}

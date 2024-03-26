using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionPanel : MonoBehaviour
{
    [SerializeField] TMP_Text conditionText;
    [SerializeField] TMP_Text beforeText;
    [SerializeField] TMP_Text duringText;
    [SerializeField] TMP_Text completedText;
    [SerializeField] Image progressBarFilledImage;
    [SerializeField] GameObject progressBar;

    void OnEnable()
    {
        beforeText.enabled = false;
        completedText.enabled = false;
        conditionText.enabled = false;
        //Interactable.InteractablesInRangeChanged += UpdateHintTextState;
        FindObjectOfType<InteractionManager>().CurrentInteractableChanged += UpdateInteractionText;
        Interactable.AnyInteractionComplete += ShowCompletedInspectionText;
    }

    void UpdateInteractionText(Interactable interactable)
    {
        if(interactable == null) 
        {
            conditionText.enabled = false;
            beforeText.enabled = false;
        }
        else
        {
            var interactionType = interactable.InteractionType;
            beforeText.SetText($"{interactionType.Hotkey} - {interactionType.BeforeInteraction}");
            beforeText.enabled = interactable.CheckConditions();
            duringText.SetText(interactionType.DuringInteraction);
            conditionText.enabled = true;
            conditionText.SetText(interactable.ConditionMessage);
        }
    }

    void OnDisable()
    {
        Interactable.InteractablesInRangeChanged -= UpdateHintTextState;
        Interactable.AnyInteractionComplete -= ShowCompletedInspectionText;
    }

    void ShowCompletedInspectionText(Interactable inspectable, string completedInspectionMessage)
    {
        completedText.SetText(completedInspectionMessage);
        completedText.enabled = true;
        float messageTime = completedInspectionMessage.Length / 3;
        messageTime = Mathf.Clamp(messageTime, 3f, 15f);
        StartCoroutine(FadeCompletedText(messageTime));
    }

    IEnumerator FadeCompletedText(float messageTime)
    {
        completedText.alpha = 1f;
        while (completedText.alpha > 0f)
        {
            yield return null;
            completedText.alpha -= Time.deltaTime / messageTime;
        }

        completedText.enabled = false;
    }

    void UpdateHintTextState(bool enableHint)
    {
        beforeText.enabled = enableHint;
    }

    void Update()
    {
        if (InteractionManager.Interacting)
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

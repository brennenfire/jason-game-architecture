using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEditor.MPE;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    
    [SerializeField] TMP_Text storyText;
    [SerializeField] Button[] choiceButtons;

    Story story;
    CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        ToggleCanvasOff();
    }

    [ContextMenu("StartCoroutine Dialog")]
    public void StartDialog(TextAsset dialog)
    {
        story = new Story(dialog.text);
        RefreshView();
        ToggleCanvasOn();
    }

    void ToggleCanvasOn()
    {
        canvasGroup.alpha = 0.5f;
        canvasGroup.interactable= true;
        canvasGroup.blocksRaycasts = true;
    }

    void ToggleCanvasOff()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    void RefreshView()
    {
        StringBuilder storyTextBuilder = new StringBuilder();
        while(story.canContinue)
        {
            storyTextBuilder.AppendLine(story.Continue());
            HandleTags();
        }

        storyText.SetText(storyTextBuilder);

        if (story.currentChoices.Count == 0)
        {
            ToggleCanvasOff();
        }
        else
        {
            ShowChoiceButtons();
        }

    }

    private void ShowChoiceButtons()
    {
        for (int i = 0; choiceButtons.Length > i; i++)
        {
            var button = choiceButtons[i];
            button.gameObject.SetActive(i < story.currentChoices.Count);
            button.onClick.RemoveAllListeners();
            if (i < story.currentChoices.Count)
            {
                var choice = story.currentChoices[i];
                button.GetComponentInChildren<TMP_Text>().SetText(choice.text);
                button.onClick.AddListener(() =>
                {
                    story.ChooseChoiceIndex(choice.index);
                    RefreshView();
                });
            }
        }
    }

    void HandleTags()
    {
        foreach(var tag in story.currentTags) 
        {
            Debug.Log(tag);
            if(tag.StartsWith("E."))
            {
                string eventName = tag.Remove(0, 2);
                GameEvent.RaiseEvent(eventName);
            }
        }
    }

    /*
    void OpenDoor()
    {
        animator.SetTrigger("Open");
    }
    */
}

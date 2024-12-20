using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEditor.MPE;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : ToggleablePanel
{
    
    [SerializeField] TMP_Text storyText;
    [SerializeField] Button[] choiceButtons;

    Story story;
    
    [ContextMenu("StartCoroutine Dialog")]
    public void StartDialog(TextAsset dialog)
    {
        story = new Story(dialog.text);
        RefreshView();
        Show();
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
            Hide();
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
            else if(tag.StartsWith("Q."))
            {
                string questName = tag.Remove(0, 2);
                QuestManager.Instance.AddQuestByName(questName);
            }
            else if (tag.StartsWith("F."))
            {
                // F.BrokenPanelsInspected.9
                //string flagName = tag.Remove(0, 2);
                var values = tag.Split(".");
                FlagManager.Instance.Set(values[1], values[2]);
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

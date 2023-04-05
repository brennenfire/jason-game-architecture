using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    
    [SerializeField] TMP_Text storyText;
    [SerializeField] Button[] choiceButtons;

    Story story;

    [ContextMenu("StartCoroutine Dialog")]
    public void StartDialog(TextAsset dialog)
    {
        story = new Story(dialog.text);
        RefreshView();
    }

    void RefreshView()
    {
        StringBuilder storyTextBuilder = new StringBuilder();
        while(story.canContinue)
        {
            storyTextBuilder.AppendLine(story.Continue());

            storyText.SetText(storyTextBuilder);
        }

        for(int i = 0; choiceButtons.Length > i; i++) 
        {
            var button = choiceButtons[i];
            button.gameObject.SetActive(i < story.currentChoices.Count);
            button.onClick.RemoveAllListeners();
            if(i < story.currentChoices.Count)
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
}

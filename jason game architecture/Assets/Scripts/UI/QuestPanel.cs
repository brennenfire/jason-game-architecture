using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestPanel : ToggleablePanel
{
    [SerializeField] Quest selectedQuest;
    [SerializeField] Step selectedStep;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text descriptionText;
    [SerializeField] TMP_Text currentObjectiveText;
    [SerializeField] Image iconImage;

    [ContextMenu("Bind)")] 
    public void Bind()
    {
        iconImage.sprite = selectedQuest.Sprite;
        nameText.SetText(selectedQuest.DisplayName);
        descriptionText.SetText(selectedQuest.Description);

        selectedStep = selectedQuest.steps.FirstOrDefault();
        DisplayStepObjectives();
    }

    internal void SelectQuest(Quest quest)
    {
        selectedQuest = quest;
        Bind();
        Show();
    }

    private void DisplayStepObjectives()
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendLine(selectedStep.Instructions);
        if (selectedStep != null)
        {
            foreach (var objective in selectedStep.Objectives)
            {
                builder.AppendLine(objective.ToString());

            }
        }
        currentObjectiveText.SetText(builder.ToString());
    }
}

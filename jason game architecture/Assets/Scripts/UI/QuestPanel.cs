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
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text descriptionText;
    [SerializeField] TMP_Text currentObjectiveText;
    [SerializeField] Image iconImage;

    Step selectedStep => selectedQuest.CurrentStep;

    [ContextMenu("Bind)")] 
    public void Bind()
    {
        iconImage.sprite = selectedQuest.Sprite;
        nameText.SetText(selectedQuest.DisplayName);
        descriptionText.SetText(selectedQuest.Description);

        DisplayStepObjectives();
    }
   
    public void SelectQuest(Quest quest)
    {
        
        if(selectedQuest != null) 
        {
            selectedQuest.Changed -= DisplayStepObjectives;
        }
        
        selectedQuest = quest;
        Bind();
        Show();
        //DisplayStepObjectives();

        selectedQuest.Changed += DisplayStepObjectives;
    }

    private void DisplayStepObjectives()
    {
        StringBuilder builder = new StringBuilder();
        if (selectedStep != null)
        {
            builder.AppendLine(selectedStep.Instructions);
            foreach (var objective in selectedStep.Objectives)
            {
                string rgb = objective.IsCompleted ? "green" : "grey";
                builder.AppendLine($"<color={rgb}>{objective}</color>");

            }
        }
        currentObjectiveText.SetText(builder.ToString());
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }
    List<Quest> activeQuests = new List<Quest>();
    [SerializeField] QuestPanel questPanel;
    [SerializeField] List<Quest> allQuests;

    void Awake()
    {
        Instance = this;    
    }

    public void AddQuest(Quest quest)
    {
        activeQuests.Add(quest);
        questPanel.SelectQuest(quest);
    }

    internal void AddQuestByName(string questName)
    {
        var quest = allQuests.FirstOrDefault(t => t.name == questName);
        if (quest != null)
        {
            AddQuest(quest);
        }
        else
            Debug.LogError("missing quest");
    }
}

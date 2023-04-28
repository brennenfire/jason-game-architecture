using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }
    List<Quest> activeQuests = new List<Quest>();
    [SerializeField] QuestPanel questPanel;

    void Awake()
    {
        Instance = this;    
    }

    public void AddQuest(Quest quest)
    {
        activeQuests.Add(quest);
        questPanel.SelectQuest(quest);
    }
}

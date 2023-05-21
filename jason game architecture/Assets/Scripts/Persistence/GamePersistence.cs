using System.Collections;
using UnityEngine;

public class GamePersistence : MonoBehaviour
{
    GameData gameData;

    void Start()
    {
        LoadGameFlags();
    }

    void OnDisable()
    {
        SaveGameFlags();    
    }

    void SaveGameFlags()
    {
        Debug.Log("Saving Game Data");
        var json = JsonUtility.ToJson(gameData);
        Debug.Log(json);
        Debug.Log("Saving Game Data Complete");
    }

    void LoadGameFlags()
    {
        gameData = new GameData();
        FlagManager.Instance.Bind(gameData.GameFlagDatas);
    }
}

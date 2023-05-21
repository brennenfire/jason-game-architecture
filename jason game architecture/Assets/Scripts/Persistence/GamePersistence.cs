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
        PlayerPrefs.SetString("GameData", json);
        Debug.Log(json);
        Debug.Log("Saving Game Data Complete");
    }

    void LoadGameFlags()
    {
        var json = PlayerPrefs.GetString("GameData");
        gameData = JsonUtility.FromJson<GameData>(json);
        if (gameData == null)
        {
            new GameData();
        }
        FlagManager.Instance.Bind(gameData.GameFlagDatas);
    }
}

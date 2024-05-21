using System.Collections;
using System.Linq;
using UnityEngine;

public class GamePersistence : MonoBehaviour
{
    public GameData gameData;

    void Start()
    {
        LoadGame();
    }

    void OnDisable()
    {
        SaveGame();  
    }

    void SaveGame()
    {
        Debug.Log("Saving Game Data");
        var json = JsonUtility.ToJson(gameData);
        PlayerPrefs.SetString("GameData", json);
        Debug.Log(json);
        Debug.Log("Saving Game Data Complete");
    }

    void LoadGame()
    {
        var json = PlayerPrefs.GetString("GameData");
        gameData = JsonUtility.FromJson<GameData>(json);
        if (gameData == null)
        {
            gameData = new GameData();
        }

        var players = FindObjectsOfType<Player>();
        foreach ( Player player in players ) 
        {
            var playerData = gameData.PlayerDatas.FirstOrDefault(t => t.PlayerName == player.name);
            if(playerData == null)
            {
                playerData = new PlayerData() { PlayerName = player.name };
                gameData.PlayerDatas.Add(playerData);
            }

            player.Bind(playerData);
        }

        Inventory.Instance.Bind(gameData.SlotDatas);
        FlagManager.Instance.Bind(gameData.GameFlagDatas);
        InteractionManager.Bind(gameData.InteractableDatas);
        PlacementManager.Instance.Bind(gameData.PlaceablesDatas);
        FindObjectOfType<PlayerPicker>().Initialize();
    }
}

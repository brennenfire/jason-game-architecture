using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
    public List<GameFlagData> GameFlagDatas;
    public List<InteractableData> InteractableDatas;
    public List<SlotData> SlotDatas;
    public List<PlaceableData> PlaceablesDatas;
    public List<PlayerData> PlayerDatas;

    public GameData()
    {
        PlayerDatas = new List<PlayerData>();
        GameFlagDatas = new List<GameFlagData>();
        InteractableDatas = new List<InteractableData>();
        //GameFlagDatas.Add(new GameFlagData() { Value = "jason 1", Name = "flagname" });
        SlotDatas = new List<SlotData>();
        PlaceablesDatas = new List<PlaceableData>();
    }

}

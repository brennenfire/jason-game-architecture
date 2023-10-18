using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
    public List<GameFlagData> GameFlagDatas;
    public List<InspectableData> InspectableDatas;
    public List<SlotData> SlotDatas;
    public List<PlaceableData> PlaceablesDatas;

    public GameData()
    {
        GameFlagDatas = new List<GameFlagData>();
        InspectableDatas = new List<InspectableData>();
        //GameFlagDatas.Add(new GameFlagData() { Value = "jason 1", Name = "flagname" });
        SlotDatas = new List<SlotData>();
        PlaceablesDatas = new List<PlaceableData>();
    }

}

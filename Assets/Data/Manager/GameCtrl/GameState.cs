using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameState
{
    public MapResult SelectedMapData;
    public MapStatisticsData MapStatistics;

    public void SetMapStatisticsData(MapStatisticsData mapStatisticsData) => this.MapStatistics = mapStatisticsData;

    public void ResetData()
    {
        SelectedMapData = null;
        MapStatistics = new MapStatisticsData();
    }
}

using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string playerFilePath = Application.persistentDataPath + "/playerdata.json";

    public static void SavePlayerData(PlayerData playerData)
    {
        string json = JsonUtility.ToJson(playerData, true);
        File.WriteAllText(playerFilePath, json);
        Debug.Log("Player data saved to: " + playerFilePath);
    }

    public static PlayerData LoadPlayerData()
    {
        if (File.Exists(playerFilePath))
        {
            string json = File.ReadAllText(playerFilePath);
            return JsonUtility.FromJson<PlayerData>(json);
        }
        Debug.Log("No player data found.");
        return null;
    }

    public static void DeletePlayerData()
    {
        try
        {
            if (File.Exists(playerFilePath))
            {
                File.Delete(playerFilePath);
                Debug.Log("Player data has been deleted.");
            }
            else
            {
                Debug.Log("No player data found to delete at: " + playerFilePath);
            }
        }
        catch (IOException ex)
        {
            Debug.LogError("Error deleting player data: " + ex.Message);
        }
    }



    
 
}

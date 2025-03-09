using System.IO;
using UnityEngine;
using Newtonsoft.Json;
public class SaveManager : MonoBehaviour
{
    private static string savePath = Application.persistentDataPath + "/saveData.json";
    
    public static void SaveGame(GameData data)
    {
        Debug.Log("Saving Data");
        string json = JsonConvert.SerializeObject(data);
        Debug.Log("Game data : " + json);
        File.WriteAllText(savePath, json);
    }

    public static GameData LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            return JsonConvert.DeserializeObject<GameData>(json);
        }
        return null;
    }
}

using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void NewPlayerData(Player player)
    {
        BinaryFormatter bFormatter = new BinaryFormatter();
        //string path = Application.persistentDataPath + "/" + player.saveName;    //saveGame = Save#.sav
        string path = Application.dataPath + "/" + player.saveName;    //saveGame = Save#.sav
        FileStream fStream = new FileStream(path, FileMode.Create); //FileMode.Create will override an existing save file

        if (File.Exists(path))
        {
            Debug.LogError("NEW GAME: Created new save");
        }
        else
        {
            Debug.LogError("NEW GAME: Error create new save");
        }

            /*
            PlayerData data = new PlayerData(player);

            bFormatter.Serialize(fStream, data);

            if (File.Exists(path))
            {
                Debug.Log("NEW GAME: New save successful @ " + path);
            }

            */
        fStream.Close();
        SavePlayerData(player);
    }

    public static void SavePlayerData(Player player)
    {
        BinaryFormatter bFormatter = new BinaryFormatter();
        //string path = Application.persistentDataPath + "/" + player.saveName;    //saveGame = Save#.sav
        string path = Application.dataPath + "/" + player.saveName;    //saveGame = Save#.sav
        FileStream fStream = new FileStream(path, FileMode.Open); //Open an existing save file and modify

        PlayerData data = new PlayerData(player);

        bFormatter.Serialize(fStream, data);

        if (File.Exists(path))
        {
            Debug.LogError("SAVE GAME: Game save successful @ " + path);
        }

        fStream.Close();
    }

    public static PlayerData LoadPlayerData(string saveGame)
    {
        //string path = Application.persistentDataPath + "/" + saveGame;
        string path = Application.dataPath + "/" + saveGame;

        if (File.Exists(path))
        {
            BinaryFormatter bFormatter = new BinaryFormatter();
            FileStream fStream = new FileStream(path, FileMode.Open); //Open an existing save file and modify

            PlayerData data = bFormatter.Deserialize(fStream) as PlayerData;
            fStream.Close();

            Debug.LogError("LOAD GAME: Game load successful @ " + path);
            

            return data;
        }
        else
        {
            Debug.LogError("ERROR: Save Game does not exist @ " + path);
            return null;
        }
    }
}

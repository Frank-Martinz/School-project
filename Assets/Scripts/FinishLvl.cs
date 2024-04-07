using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class FinishLvl : MonoBehaviour
{
    public TextAsset textJSON;

    public PlayerInfo pl_info = new PlayerInfo();
    
    [System.Serializable]   
    public class PlayerInfo
    {
        public int current_lvl;
    }



    public void SaveData()
    {   
        PlayerInfo pl_info = JsonUtility.FromJson<PlayerInfo>(textJSON.text);

        if (pl_info.current_lvl > 12) { pl_info.current_lvl = 12; }

        PlayerInfo updated_info = new PlayerInfo();
        updated_info.current_lvl = pl_info.current_lvl + 1;
        Debug.Log($"Сохранён уровень: {updated_info.current_lvl}");
        string json = JsonUtility.ToJson(updated_info, true);
        File.WriteAllText(Application.dataPath + "/Data/Player_info.json", json);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.IO;
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


    void Start()
    {
        pl_info = JsonUtility.FromJson<PlayerInfo>(textJSON.text);
    }

    public void SaveData()
    {
        pl_info.current_lvl += 1;
        Debug.Log(pl_info.current_lvl);

        if (pl_info.current_lvl > 12) { pl_info.current_lvl = 12; }

        string json = JsonUtility.ToJson(pl_info, true);
        File.WriteAllText(Application.dataPath + "/Data/Player_info.json", json);
    }
}

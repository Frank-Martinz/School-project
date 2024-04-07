using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;


public class Download_next_lvl : MonoBehaviour
{
    public TextAsset textJSON;

    public PlayerInfo pl_info = new PlayerInfo();
    
    [System.Serializable]
    public class PlayerInfo
    {
        public int current_lvl;
    }

    public void StartNextLvl()
    {
        PlayerInfo pl_info = JsonUtility.FromJson<PlayerInfo>(textJSON.text);
        if (pl_info.current_lvl > 12) { pl_info.current_lvl = 12; }

        Debug.Log($"{pl_info.current_lvl + 1}: загружается уровень");
        if (pl_info.current_lvl != 12) { SceneManager.LoadScene(pl_info.current_lvl + 1);}
        else {SceneManager.LoadScene(0);}
    }
}

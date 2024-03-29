using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Start_new_game_button : MonoBehaviour
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

    public void StartNewGame()
    {
        PlayerInfo pl_info = new PlayerInfo();
        pl_info.current_lvl = 1;

        string json = JsonUtility.ToJson(pl_info, true);
        File.WriteAllText(Application.dataPath + "/Data/Player_info.json", json);
        SceneManager.LoadScene(1);
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(pl_info.current_lvl);
    }
}

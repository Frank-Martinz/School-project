using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Start_new_game_button : MonoBehaviour
{
    public TextAsset textJSON;
    
    [System.Serializable]
    public class PlayerInfo
    {
        public int current_lvl;
    }

    public void StartNewGame()
    {
        PlayerInfo new_info = new PlayerInfo();
        new_info.current_lvl = 1;

        string json = JsonUtility.ToJson(new_info, true);
        File.WriteAllText(Application.dataPath + "/Data/Player_info.json", json);
        SceneManager.LoadScene(1);
    }

    public void ContinueGame()
    {
        PlayerInfo pl_info = JsonUtility.FromJson<PlayerInfo>(textJSON.text);
        
        SceneManager.LoadScene(pl_info.current_lvl);
    }
}

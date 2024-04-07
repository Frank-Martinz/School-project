using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartLevelScript : MonoBehaviour
{
    public TextAsset textJSON;
    public TextAsset player_json;

    public Text task_text;
    public Buttons_script confirm_button;

    public Levels myLevelsList = new Levels();
    
    [System.Serializable]
    public class PlayerInfo
    {
        public int current_lvl;
    }
    
    [System.Serializable]
    public class LevelInfo
    {
        public string Level;
        public string Question;
        public string Answer;
    }

    public class Levels
    {
        public LevelInfo[] levels;
    }


    void Start()
    {
        myLevelsList = JsonUtility.FromJson<Levels>(textJSON.text);
        PlayerInfo pl_info = JsonUtility.FromJson<PlayerInfo>(player_json.text);
        Debug.Log(pl_info.current_lvl);
        string actual_answer = myLevelsList.levels[pl_info.current_lvl - 1].Answer;
        string question = myLevelsList.levels[pl_info.current_lvl - 1].Question;
        confirm_button.actual_answer = actual_answer;
        task_text.text = question;
    }
}

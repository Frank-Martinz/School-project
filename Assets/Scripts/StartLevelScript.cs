using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartLevelScript : MonoBehaviour
{
    public TextAsset textJSON;
    public Text task_text;
    public Buttons_script confirm_button;

    public Levels myLevelsList = new Levels();
    
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
        string actual_answer = myLevelsList.levels[0].Answer;
        string question = myLevelsList.levels[0].Question;
        confirm_button.actual_answer = actual_answer;
        task_text.text = question;
    }
}

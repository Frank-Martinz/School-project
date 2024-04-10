using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class FinishLvl : MonoBehaviour
{
    public int next_lvl;

    public void SaveData()
    {   
        string path = @"Assets\Data\Player_info.txt";
        string[] text = File.ReadAllLines(path);
        int finished_lvl = Convert.ToInt16(text[1].Split(':')[1]);
        string saving_data;
        if (next_lvl > finished_lvl) { saving_data = $"player_actual_lvl:{next_lvl}\nplayer_has_finished_lvl:{next_lvl}"; }
        else { saving_data = $"player_actual_lvl:{next_lvl}\nplayer_has_finished_lvl:{finished_lvl}";}
        
        File.WriteAllText(path, saving_data);
    }
}

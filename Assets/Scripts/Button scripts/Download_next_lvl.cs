using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;


public class Download_next_lvl : MonoBehaviour
{
    public int next_lvl;

    public void StartNextLvl()
    {
        string path = @"Assets\Data\Player_info.txt";
        string[] text = File.ReadAllLines(path);
        int current_lvl = Convert.ToInt16(text[0].Split(':')[1]);
        if (current_lvl > 12) { current_lvl = 12; }

        if (current_lvl != 12) { SceneManager.LoadScene(next_lvl);}
        else {SceneManager.LoadScene(0);}
    }
}

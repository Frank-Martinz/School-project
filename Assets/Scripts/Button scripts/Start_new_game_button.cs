using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Start_new_game_button : MonoBehaviour
{
    public Canvas ChoosingLvlCanvas;
    public Canvas Menu;

    public void OpenChoosingLvlCanvas()
    {  
       ChoosingLvlCanvas.gameObject.SetActive(true); 
       Menu.gameObject.SetActive(false);
    }

    public void ContinueGame()
    {
        string path = @"Assets\Data\Player_info.txt";
        string[] text = File.ReadAllLines(path);
        int current_lvl = Convert.ToInt16(text[0].Split(':')[1]);
        SceneManager.LoadScene(current_lvl);
    }
}

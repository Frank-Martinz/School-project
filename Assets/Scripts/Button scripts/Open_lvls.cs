using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;

public class Open_lvls : MonoBehaviour
{
    public GameObject CL_canvas;

    void Start()
    {
        string path = @"Assets/Data/Player_info.txt";
        string[] text = File.ReadAllLines(path);
        int max_lvl = Convert.ToInt16(text[1].Split(':')[1]);

        GameObject[] allChildren = new GameObject[CL_canvas.transform.childCount];

        for (int i = 0; i < allChildren.Length; i++)
        {
            allChildren[i] = CL_canvas.transform.GetChild(i).gameObject;  
        }

        for (int i = 0; i < allChildren.Length; i++)
        {
            if (allChildren[i].gameObject.name != "GetBackBut")
            {
                if (max_lvl >= i + 1)
                {
                    allChildren[i].GetComponent<Button>().enabled = true;
                }
                else
                {
                allChildren[i].GetComponent<Button>().enabled = false;
                allChildren[i].GetComponent<Image>().color = Color.grey;
                allChildren[i].transform.GetChild(1).gameObject.GetComponent<Image>().color = Color.grey;
                }
            }
        }
    }
}

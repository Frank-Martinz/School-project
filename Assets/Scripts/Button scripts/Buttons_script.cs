using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Buttons_script : MonoBehaviour
{
    public Button but;
    public InputField answer_field;
    public StartAnimation animatedobj;
    public Player_movement pm;

    public string actual_answer;

    
    public void CheckAnswer()
    { 
        string player_answer = answer_field.text.ToUpper();
        answer_field.text = "";
        if (actual_answer.Equals(player_answer))
        {
            animatedobj.StartAnim();
            pm.LeaveComputer(true);
        }
        else
        {
            Debug.Log("Неправда");
        }
    }
}

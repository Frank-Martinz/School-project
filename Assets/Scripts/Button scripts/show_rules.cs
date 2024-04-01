using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class show_rules : MonoBehaviour
{
    public Canvas rules_canvas;
    public Canvas menu_canvas;


    public void Show_rules()
    {
        menu_canvas.gameObject.SetActive(false);
        rules_canvas.gameObject.SetActive(true);
    }
}

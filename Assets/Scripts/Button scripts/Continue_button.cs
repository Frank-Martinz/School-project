using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Continue_button : MonoBehaviour
{
    public Button continue_but;
    public Canvas menu_can;

    public void Continue_lvl()
    {
        Time.timeScale = 1f;
        menu_can.gameObject.SetActive(false);
    }
}

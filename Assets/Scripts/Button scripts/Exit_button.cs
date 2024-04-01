using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Exit_button : MonoBehaviour
{
    public Button exit_but;

    public void Exit_to_Menu()
    {
        SceneManager.LoadScene(0);
    }
}

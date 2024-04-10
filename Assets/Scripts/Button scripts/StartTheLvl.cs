using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartTheLvl : MonoBehaviour
{
    public int lvl;

    public void StartLvl()
    {
        SceneManager.LoadScene(lvl);
    }
}

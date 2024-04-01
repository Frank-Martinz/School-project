using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Next_prev_rule : MonoBehaviour
{
    public Canvas Rules_canvas;
    public Canvas Menu;

    private int rule_now = 1;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Rules_canvas.gameObject.activeSelf)
            {
                Rules_canvas.gameObject.SetActive(false);
                Menu.gameObject.SetActive(true);
            }
        }        
    }

    public void Next_rule()
    {
        int id_next_rule = rule_now + 1;
        if (id_next_rule > 2)
        {
            id_next_rule = 1;
        }
        var im_rule_now = Rules_canvas.transform.Find($"Rule_{rule_now}");
        var im_rule_next = Rules_canvas.transform.Find($"Rule_{id_next_rule}");

        im_rule_now.gameObject.SetActive(false);
        im_rule_next.gameObject.SetActive(true);
        rule_now = id_next_rule;
    }

    public void Prev_rule()
    {
        int id_next_rule = rule_now - 1;
        if (id_next_rule < 1)
        {
            id_next_rule = 2;
        }
        var im_rule_now = Rules_canvas.transform.Find($"Rule_{rule_now}");
        var im_rule_next = Rules_canvas.transform.Find($"Rule_{id_next_rule}");
        
        im_rule_now.gameObject.SetActive(false);
        im_rule_next.gameObject.SetActive(true);
        rule_now = id_next_rule;
    }
}

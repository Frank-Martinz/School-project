using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class falling_object_script : MonoBehaviour
{   
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag != "Player") { transform.gameObject.SetActive(false); }
    }

}

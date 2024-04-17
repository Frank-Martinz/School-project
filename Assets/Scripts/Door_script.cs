using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_script : MonoBehaviour
{
    public Sprite opened_door;

    public void OpenDoor()
    {
        Destroy(GetComponent<BoxCollider2D>());
        Destroy(GetComponent<BoxCollider2D>());
        gameObject.GetComponent<SpriteRenderer>().sprite =  opened_door;       
    }
}

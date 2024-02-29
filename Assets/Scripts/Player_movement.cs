using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_movement : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D player_rb;

    public Text press_but_text;
    public Image Back_ground;

    public float speed = 5f;
    public float max_speed = 7f;

    public bool CanJump = false;
    public bool can_interact = false;
    public bool is_busy = false;


    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Ground")
        {
            CanJump = true;
        }
    }
    
    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.tag == "Ground")
        {
            CanJump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Computer")
        {
            press_but_text.gameObject.SetActive(true);
            can_interact = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Computer")
        {
            press_but_text.gameObject.SetActive(false);
            can_interact = false;
        }
    }

    private void Update() {
        if (is_busy && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E)))
        {
            is_busy = false;
            Back_ground.gameObject.SetActive(false);
        }
        else if (can_interact && Input.GetKeyDown(KeyCode.E))
        {
            Back_ground.gameObject.SetActive(true);
            player_rb.velocity = Vector2.zero;
            is_busy = true;
        }
    }

    private void FixedUpdate() 
    {
        if (Input.GetKey(KeyCode.D))
        {
            player_rb.AddForce(Vector2.right * speed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            player_rb.AddForce(Vector2.left * speed);
        }

        if (Input.GetKey(KeyCode.Space) && CanJump)
        {
            player_rb.AddForce(Vector2.up * 50);
        }

        if (player_rb.velocity.x > max_speed)
        {
            player_rb.velocity = new Vector2(max_speed, player_rb.velocity.y);
        }
        else if (player_rb.velocity.x < -max_speed)
        {
            player_rb.velocity = new Vector2(-max_speed, player_rb.velocity.y);
        }
    }
}

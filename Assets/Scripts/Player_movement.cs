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
    public Text task_text;

    public float speed = 5f;
    public float max_speed = 7f;
    public float x_speed = 0f;
    public float y_speed = 0f;
    public float cooldown = 0f;

    public bool CanJump = false;
    public bool can_interact = false;
    public bool is_busy = false;
    public bool is_touching_obst = false;
    public bool right_side_obst = false;
    public bool left_side_obst = false;
    public bool top_side_obst = false;
    public bool near_to_vent = false;
    public bool in_vent = false;
    public bool level_completed = false;


    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Ground")
        {
            CanJump = true;
        }
        if (other.gameObject.tag == "Obstacle")
        {
            CanJump = true;
            is_touching_obst = true;
            player_rb.velocity = new Vector2(0, 0);
            right_side_obst = false;
            left_side_obst = false;
            top_side_obst = false;
            GameObject wall = other.gameObject;
            float top_wall = wall.transform.position.y + (wall.transform.localScale.y / 2);
            float player_bottom = player.transform.position.y - (player.transform.localScale.y / 2) + 0.1f; 
            if (top_wall < player_bottom)
            {
                is_touching_obst = false;
                top_side_obst = true;
            }
            else if (other.gameObject.transform.position.x < player.transform.position.x)
            {
                right_side_obst = true;
                cooldown = 0;
            }
            else 
            {
                left_side_obst = true;
                cooldown = 0;
            }
        }

    }

    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.tag == "Ground")
        {
            CanJump = true;
        }
        if (other.gameObject.tag == "Obstacle")
        {
            CanJump = true;
        }
    }
    
    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.tag == "Ground")
        {
            CanJump = false;
        }
        if (other.gameObject.tag == "Obstacle")
        {
            is_touching_obst = false;
            CanJump = false;
            right_side_obst = false;
            left_side_obst = false;
            top_side_obst = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Computer" && !level_completed)
        {

            press_but_text.gameObject.SetActive(true);
            press_but_text.text = "Нажми E чтобы взаимодействовать";
            can_interact = true;
        }

        if (other.gameObject.tag == "Ventilation")
        {
            near_to_vent = true;
            press_but_text.gameObject.SetActive(true);
            press_but_text.text = "Нажми E чтобы залезть в вентиляцию";
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Computer")
        {
            press_but_text.gameObject.SetActive(false);
            can_interact = false;
        }
        if (other.gameObject.tag == "Ventilation")
        {
            near_to_vent = false;
            press_but_text.gameObject.SetActive(false);
            press_but_text.text = "Нажми E чтобы залезть в вентиляцию";
        }

    }

    private void Update() {
        if (is_busy && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E)))
        {
            LeaveComputer();
        }
        else if (can_interact && Input.GetKeyDown(KeyCode.E))
        {
            Back_ground.gameObject.SetActive(true);
            task_text.gameObject.SetActive(true);
            player_rb.velocity = Vector2.zero;
            is_busy = true;
        }

        if (near_to_vent && Input.GetKeyDown(KeyCode.E) && !in_vent)
        {
            in_vent = true;
            int VentLayer = LayerMask.NameToLayer("Ventilation");
            player.transform.Translate(new Vector3(0, 2f, 0));
            player.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            max_speed = 3f;
            player.layer = VentLayer;
        }
        else if (near_to_vent && Input.GetKeyDown(KeyCode.E) && in_vent)
        {
            in_vent = false;
            int DefaultLayer = LayerMask.NameToLayer("Default");
            player.transform.Translate(new Vector3(0, -1f, 0));
            player.transform.localScale = new Vector3(1f, 2f, 1f);
            max_speed = 7f;
            player.layer = DefaultLayer;
        }
    }

    private void FixedUpdate() 
    {
        if (!is_busy)
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
                    if (cooldown <= 0) 
                    {
                        cooldown = 0.8f;
                        player_rb.AddForce(Vector2.up * 130);
                        if (is_touching_obst)
                        {
                            if (right_side_obst) { 
                                player_rb.AddForce(Vector2.right * 50); 
                                player_rb.AddForce(Vector2.up * 20);}
                            else if (left_side_obst) { 
                                player_rb.AddForce(Vector2.left * 50); 
                                player_rb.AddForce(Vector2.up * 20);}
                            
                        }
                    }
                }
        }
        if (cooldown > 0) {cooldown -= Time.deltaTime;}
        if (player_rb.velocity.x > max_speed)
        {
            player_rb.velocity = new Vector2(max_speed, player_rb.velocity.y);
        }
        else if (player_rb.velocity.x < -max_speed)
        {
            player_rb.velocity = new Vector2(-max_speed, player_rb.velocity.y);
        }
        if (is_touching_obst)
        {
            if (player_rb.velocity.y > 0 && player_rb.velocity.x == 0)
            {
                player_rb.velocity = new Vector2(player_rb.velocity.y * 0.8f, 0);
            }
        }
    }

    public void LeaveComputer(bool level_comp=false)
    {
        is_busy = false;
        Back_ground.gameObject.SetActive(false);
        task_text.gameObject.SetActive(false);
        if (level_comp)
        {
            level_completed = true;
            can_interact = false;
            press_but_text.gameObject.SetActive(false);
        }
    }
}

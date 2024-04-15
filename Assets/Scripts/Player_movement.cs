using System;
using UnityEngine;
using UnityEngine.UI;

public class Player_movement : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D player_rb;
    public SpriteRenderer player_sprite;
    public Animator player_animator;

    public FinishLvl finish_lvl_script;

    public AnimationClip player_stay_anim;
    public AnimationClip player_running_anim;
    public AnimationClip player_sliding_anim;
    public AnimationClip player_give_up;

    public Canvas menu_can;
    public Canvas go_next_lvl;
    public Canvas game_over_canvas;

    public Text press_but_text;
    public Image Back_ground;
    public Text task_text;
    public Image ending_game_shield;

    public float speed = 5f;
    public float max_speed = 7f;
    public float x_speed = 0f;
    public float y_speed = 0f;
    public float cooldown = 0f;

    public bool CanJump = false;
    public bool CanClimb = false;
    public bool can_interact = false;
    public bool is_busy = false;
    public bool is_touching_obst = false;
    public bool right_side_obst = false;
    public bool left_side_obst = false;
    public bool top_side_obst = false;
    public bool near_to_vent = false;
    public bool in_vent = false;
    public bool level_completed = false;
    public bool game_over = false;

    public GameObject last_touch;
    public float last_touch_cooldown;

    void Start()
    {
        Time.timeScale = 1f;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Ground")
        {
            CanJump = true;
            last_touch = null;
        }
        if (other.gameObject.tag == "Obstacle" && last_touch != other.gameObject)
        {
            CanJump = true;
            is_touching_obst = true;
            last_touch = other.gameObject;
            player_rb.velocity = new Vector2(0, 0);
            right_side_obst = false;
            left_side_obst = false;
            top_side_obst = false;
            GameObject wall = other.gameObject;
            last_touch_cooldown = 1f;
            float top_wall = wall.transform.position.y + (wall.transform.localScale.y / 2);
            float player_bottom = player.transform.position.y - (player.transform.localScale.y / 2);
            if (top_wall < player_bottom)
            {
                is_touching_obst = false;
                top_side_obst = true;
            }
            else if (other.gameObject.transform.position.x < player.transform.position.x)
            {
                right_side_obst = true;
                player_sprite.flipX = true;
                cooldown = 0;
            }
            else 
            {
                left_side_obst = true;
                player_sprite.flipX = false;
                cooldown = 0;
            }
        }
        if (other.gameObject.tag == "Ladder")
        {
            player_rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
            CanClimb = true;
        }
        if (other.gameObject.tag == "Falling_obs")
        {
            ending_game_shield.color = new Color(0, 0, 0, 1);
            ShowGameOverCan();
        }

    }

    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.tag == "Ground")
        {
            CanJump = true;
        }
        if (other.gameObject.tag == "Obstacle" && last_touch != other.gameObject)
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
        if (other.gameObject.tag == "Ladder")
        {   
            CanClimb = false;
            player_rb.constraints = RigidbodyConstraints2D.None;

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

        if (other.gameObject.tag == "Finish")
        {
            LeaveTheLevel();
        }

        if (other.gameObject.tag == "Obstacle")
        {
            CanJump = true;
        }
        if (other.gameObject.tag == "Falling_obs")
        {
            other.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
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
        if (other.gameObject.tag == "Obstacle")
        {
            CanJump = false;
        }
    }

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !level_completed && !is_busy)
        {
            menu_can.gameObject.SetActive(!menu_can.gameObject.activeSelf);
            Time.timeScale = Convert.ToInt16(!menu_can.gameObject.activeSelf);
        }
        if (level_completed)
        {
            ending_game_shield.color = new Color(0, 0, 0, ending_game_shield.color.a + 0.01f);
        }
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
            player_rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else if (near_to_vent && Input.GetKeyDown(KeyCode.E) && in_vent)
        {
            in_vent = false;
            int DefaultLayer = LayerMask.NameToLayer("Default");
            player.transform.Translate(new Vector3(0, -1f, 0));
            player.transform.localScale = new Vector3(0.8f, 2f, 1f);
            max_speed = 7f;
            player.layer = DefaultLayer;
            player_rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    private void FixedUpdate() 
    {
        player_rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        if (!game_over)
        {
            if (!is_busy)
            {        
                if (Input.GetKey(KeyCode.D))
                {
                    player_rb.AddForce(Vector2.right * speed);
                    player_sprite.flipX = false;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    player_rb.AddForce(Vector2.left * speed);
                    player_sprite.flipX = true; 
                }
                if (Input.GetKey(KeyCode.Space) && CanClimb)
                {
                    player_rb.AddForce(Vector2.up * 10f);
                    player_rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                }
                else if (Input.GetKey(KeyCode.LeftShift) && CanClimb)
                {
                    player_rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                }
                else if (CanClimb)
                {
                    player_rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
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
            if (is_touching_obst) { SetPlayerAnimation(false, true);}
            else if (player_rb.velocity.x > 0.05f || player_rb.velocity.x < -0.05f) { SetPlayerAnimation(true); }
            else {SetPlayerAnimation(false);}
            if (player_rb.velocity.x > max_speed)
            {
                player_rb.velocity = new Vector2(max_speed, player_rb.velocity.y);
            }
            else if (player_rb.velocity.x < -max_speed)
            {
                player_rb.velocity = new Vector2(-max_speed, player_rb.velocity.y);
            }
            if (player_rb.velocity.y > 3f && CanClimb)
            {
                player_rb.velocity = new Vector2(player_rb.velocity.x, 3f);
            }
            if (is_touching_obst)
            {
                if (player_rb.velocity.y > 0 && player_rb.velocity.x == 0)
                {
                    player_rb.velocity = new Vector2(player_rb.velocity.y * 0.8f, 0);
                }
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
            can_interact = false;
            press_but_text.gameObject.SetActive(false);
        }
    }

    void Test()
    {
        Time.timeScale = 0f;
        go_next_lvl.gameObject.SetActive(true);
        game_over = true;
    }

    void LeaveTheLevel()
    {
        level_completed = true;
        finish_lvl_script.SaveData();
        Invoke("Test", 3);
    }

    void SetPlayerAnimation(bool movement_buttons = false, bool sliding = false, bool give_up = false)
    {   
        if (give_up) { player_animator.Play(player_give_up.name); }
        else if (sliding) { player_animator.Play(player_sliding_anim.name); }
        else if (movement_buttons){ player_animator.Play(player_running_anim.name); }
        else {player_animator.Play(player_stay_anim.name);}
    }

    void ShowGameOverCan()
    {
        game_over_canvas.gameObject.SetActive(true);
    }

    public void PlayerGiveUp()
    {
        game_over = true;
        SetPlayerAnimation(false, false, true);
        level_completed = true;
        Invoke("ShowGameOverCan", 3);
    }
}

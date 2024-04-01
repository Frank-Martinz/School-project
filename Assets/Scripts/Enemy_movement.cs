using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy_movement : MonoBehaviour
{
    public RaycastHit2D hit;
    public Rigidbody2D enemy;
    public bool go_left = true;
    public bool go_right = false;

    public SpriteRenderer enemy_sprite;

    public Player_movement pm;

    void FixedUpdate()
    {   
        if (go_left) 
        {
            hit = Physics2D.Raycast(transform.position, Vector2.left, 6f);
            enemy.AddForce(Vector2.left * 10f); 
            enemy_sprite.flipX = true;
        }
        else if (go_right)
        { 
            hit = Physics2D.Raycast(transform.position, Vector2.right, 6f);
            enemy.AddForce(Vector2.right * 10f);
            enemy_sprite.flipX = false;
        }
        
        if (hit)
        {
            if (hit.collider.gameObject.tag == "Obstacle" || hit.collider.gameObject.tag == "Ladder")
            {
                go_left = !go_left;
                go_right = !go_right;
                if (go_left) { Debug.DrawRay(transform.position, Vector2.left * 6f, Color.blue); }
                else { Debug.DrawRay(transform.position, Vector2.right * 6f, Color.blue); }
            }
            else if (hit.collider.gameObject.tag == "Player")
            {
                pm.PlayerGiveUp();
                go_left = false;
                go_right = false;
            }
            
            
        }
        if (enemy.velocity.x > 5f || enemy.velocity.x < -5f)
        {
            if (enemy.velocity.x > 5f) { enemy.velocity = new Vector2(5f, enemy.velocity.y); }
            else { enemy.velocity = new Vector2(-5f, enemy.velocity.y); }
            
        }
    }  
}

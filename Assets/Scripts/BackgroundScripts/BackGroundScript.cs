using UnityEngine;

public class BackGroundScript : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D player_rb;
    public float devide_index = 1250f;
    private Vector2 offset = Vector2.zero;
    private Material material;
    void Start()
    {
        material = GetComponent<Renderer>().material;
        offset = material.GetTextureOffset("_MainTex");
    }

    void FixedUpdate()
    {
        gameObject.transform.Translate(new Vector3(player.transform.position.x - gameObject.transform.position.x, 0, 0));
        offset.x += player_rb.velocity.x / devide_index;
        material.SetTextureOffset("_MainTex", offset);
    }
}

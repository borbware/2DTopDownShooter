using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] int speed = 4;
    SpriteRenderer sr; 
    void Start()
    {
        sr = GetComponentInChildren(typeof(SpriteRenderer)) as SpriteRenderer;
    }

    void Update()
    {
		Vector2 PlayerInput = Vector2.ClampMagnitude(
			new Vector2(
				Input.GetAxisRaw("Horizontal"),
				Input.GetAxisRaw("Vertical")
			),
		1f);
        anim.SetFloat("X",PlayerInput.x);
        anim.SetFloat("Y",PlayerInput.y);
        if (PlayerInput.magnitude > 0f)
        {
            anim.SetFloat("Last_X",PlayerInput.x);
            anim.SetFloat("Last_Y",PlayerInput.y);
            if (PlayerInput.x > 0)
                sr.flipX = false;
            else
                sr.flipX = true;
            anim.SetTrigger("StartWalking");
            // anim.Play("player_walk");
        } else {
            anim.SetTrigger("StartIdling");
        }
            
        transform.position += new Vector3(PlayerInput.x, PlayerInput.y, 0f) * Time.deltaTime * speed;

    }
}

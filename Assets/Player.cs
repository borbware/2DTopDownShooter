using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] GameObject appleUI;
    [SerializeField] int speed = 4;
    SpriteRenderer sr; 
    Rigidbody2D _rigidBody;
    Vector2 PlayerInput;
    float apples;
    void Start()
    {
        sr = GetComponentInChildren(typeof(SpriteRenderer)) as SpriteRenderer;
        // _rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
		PlayerInput = Vector2.ClampMagnitude(
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

    private void FixedUpdate() {
        // _rigidBody.AddForce(new Vector3(PlayerInput.x, PlayerInput.y, 0f) * Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.tag);
        if(other.gameObject.tag == "Collectible")
        {
            Destroy(other.gameObject);
            apples += 1;
            appleUI.GetComponent<UnityEngine.UI.Text>().text = apples.ToString();
        }
    }

}

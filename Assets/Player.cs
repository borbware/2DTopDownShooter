using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] GameObject appleUI;
    [SerializeField] int speed = 4;
    SpriteRenderer sr; 
    Rigidbody2D _rigidBody;
    Vector2 PlayerInput;
    bool iFrames = false;
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
        if (iFrames && Time.time % 0.2f > 0.1f)
        {
            sr.enabled = false;
        } else {
            sr.enabled = true;
        }
        transform.position += new Vector3(PlayerInput.x, PlayerInput.y, 0f) * Time.deltaTime * speed;

    }
    private void FixedUpdate() {
        // _rigidBody.AddForce(new Vector3(PlayerInput.x, PlayerInput.y, 0f) * Time.deltaTime * speed);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Collectible")
        {
            Destroy(other.gameObject);
            AddApples(1);
        } else if (!iFrames && other.gameObject.tag == "Enemy")
        {
            AddApples(-1);
            iFrames = true;
            Invoke("StopIFrames",1.0f);
        }
    }
    private void AddApples(int apple)
    {
            apples += apple;
            apples = Mathf.Max(apples, 0);
            appleUI.GetComponent<UnityEngine.UI.Text>().text = apples.ToString();
    }

    private void StopIFrames()
    {
        iFrames = false;
    }

}

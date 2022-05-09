using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player instance;

    [SerializeField] Animator anim;
    [SerializeField] int speed = 4;
    SpriteRenderer _spriteRenderer; 
    Rigidbody2D _rigidBody;
    AlongWallMovement _alongWall;
    Vector2 PlayerInput;
    bool iFrames = false;
    void Start()
    {
        _spriteRenderer = GetComponentInChildren(typeof(SpriteRenderer)) as SpriteRenderer;
        // _rigidBody = GetComponent<Rigidbody2D>();
        _alongWall = GetComponent<AlongWallMovement>();

        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        SetSpawnPosition();
        SetCameraToFollow();
    }
    void SetSpawnPosition()
    {
        if (GameManager.instance != null
        && GameManager.instance.playerPosition != Vector3.zero)
        {
            transform.position = GameManager.instance.playerPosition;
            GameManager.instance.playerPosition = Vector3.zero;
        }
    }
    void SetCameraToFollow()
    {
        Debug.Log("asfd");
        var camera = GameObject.FindGameObjectWithTag("MainCamera");
        var follow = camera.GetComponent<FollowPlayer>();
        follow.target = gameObject;
        follow.ExactFollow();
    }
    void Update()
    {
		PlayerInput = Vector2.ClampMagnitude(
			new Vector2(
				Input.GetAxisRaw("Horizontal"),
				Input.GetAxisRaw("Vertical")
			),
		1f);

        if (_alongWall != null 
        && _alongWall.wallNormal != null 
        && _alongWall.wallNormal.SqrMagnitude() > 0f)
        {
            Vector3 temp = Vector3.Cross(_alongWall.wallNormal, PlayerInput);
            PlayerInput = (Vector2)Vector3.Cross(temp, _alongWall.wallNormal);
        }


        anim.SetFloat("X",PlayerInput.x);
        anim.SetFloat("Y",PlayerInput.y);
        if (PlayerInput.magnitude > 0f)
        {
            anim.SetFloat("Last_X",PlayerInput.x);
            anim.SetFloat("Last_Y",PlayerInput.y);
            if (PlayerInput.x > 0)
                _spriteRenderer.flipX = false;
            else
                _spriteRenderer.flipX = true;
            anim.SetTrigger("StartWalking");
            // anim.Play("player_walk");
        } else {
            anim.SetTrigger("StartIdling");
        }
        if (iFrames && Time.time % 0.2f > 0.1f)
        {
            _spriteRenderer.enabled = false;
        } else {
            _spriteRenderer.enabled = true;
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
            GameManager.instance.AddApples(1);
        } else if (!iFrames && other.gameObject.tag == "Enemy")
        {
            GameManager.instance.AddApples(-1);
            iFrames = true;
            Invoke("StopIFrames",1.0f);
        }
    }
    private void StopIFrames()
    {
        iFrames = false;
    }


}

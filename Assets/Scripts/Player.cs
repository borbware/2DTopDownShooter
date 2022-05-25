using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player instance;

    public enum PlayerState
    {
        Moving,
        Talking
    }
    public PlayerState playerState = PlayerState.Moving;

    [SerializeField] Animator anim;
    [SerializeField] int speed = 4;
    [SerializeField] ParticleSystem dustParticles;
    SpriteRenderer _spriteRenderer; 
    Rigidbody2D _rigidBody;
    AlongWallMovement _alongWall;
    public Vector2 PlayerMove;
    public bool PlayerAction;
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
        var camera = GameObject.FindGameObjectWithTag("MainCamera");
        var follow = camera.GetComponent<FollowPlayer>();
        follow.target = gameObject;
        follow.ExactFollow();
    }
    void Update()
    {
		PlayerMove = Vector2.ClampMagnitude(
			new Vector2(
				Input.GetAxisRaw("Horizontal"),
				Input.GetAxisRaw("Vertical")
			),
		1f);
        PlayerAction = Input.GetButtonDown("Fire1");

        if (dustParticles.isStopped && PlayerMove.magnitude > 0.3)
        {
            dustParticles.Play();
            Debug.Log("start");
        }
        if (dustParticles.isPlaying && PlayerMove.magnitude < 0.1)
        {
            dustParticles.Stop();
            Debug.Log("stop");
        }
        var shape = dustParticles.shape;
        shape.rotation = new Vector3(
            0, 0, Mathf.Atan2(PlayerMove.y, PlayerMove.x) * Mathf.Rad2Deg - 15);
        var main = dustParticles.main;
        main.startSpeed = 2 * PlayerMove.magnitude;
        Debug.Log(shape.rotation);


        if (playerState == PlayerState.Moving)
        {
            if (_alongWall != null 
            && _alongWall.wallNormal != null 
            && _alongWall.wallNormal.SqrMagnitude() > 0f)
            {
                Vector3 temp = Vector3.Cross(_alongWall.wallNormal, PlayerMove);
                PlayerMove = (Vector2)Vector3.Cross(temp, _alongWall.wallNormal);
            }

            anim.SetFloat("X",PlayerMove.x);
            anim.SetFloat("Y",PlayerMove.y);
            if (PlayerMove.magnitude > 0f)
            {
                anim.SetFloat("Last_X",PlayerMove.x);
                anim.SetFloat("Last_Y",PlayerMove.y);
                if (PlayerMove.x > 0)
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
            transform.position += new Vector3(PlayerMove.x, PlayerMove.y, 0f) * Time.deltaTime * speed;
        }
    }
}

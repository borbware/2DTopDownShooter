using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] GameObject _player;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void ExactFollow()
    {
        transform.position = new Vector3(
            _player.transform.position.x,
            _player.transform.position.y,
            -10
        );
    }

    void LerpFollow()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            new Vector3(
                _player.transform.position.x,
                _player.transform.position.y,
                -10
            ),
            Time.deltaTime * 2);
    }

    // Update is called once per frame
    void Update()
    {
        // ExactFollow();
        LerpFollow();
    }
}

using UnityEngine;
using Pathfinding;

public class MoveEnemy : MonoBehaviour
{
    [SerializeField] GameObject _player;
    AIPath _aipath;
    AIDestinationSetter _aiDestinationSetter;
    public GameObject[] targets = new GameObject[2];
    void Start()
    {
        _aipath = GetComponent<AIPath>();
        _aiDestinationSetter = 
            GetComponent<AIDestinationSetter>();
    }

    void FollowIfClose()
    {
        var dist = Vector2.Distance(
            _player.transform.position, transform.position
            );
        if (dist < 5.0f)
            _aipath.canMove = true;
        else
            _aipath.canMove = false;    
    }

    void Update()
    {
        // FollowIfClose();
        if (targets[0] != null && Vector2.Distance(
            targets[0].transform.position, transform.position)
        < 5.0f )
            _aiDestinationSetter.target = targets[0].transform;
        else if (targets[1]!= null)
            _aiDestinationSetter.target = targets[1].transform;
    }
}

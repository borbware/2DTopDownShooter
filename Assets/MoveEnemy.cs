using UnityEngine;
using Pathfinding;

public class MoveEnemy : MonoBehaviour
{
    [SerializeField] GameObject _player;
    AIPath _aipath;
    AIDestinationSetter _aiDestinationSetter;
    public GameObject[] targets = new GameObject[2];
    int targetIndex;
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
        if (_player != null 
        && Vector2.Distance(_player.transform.position, transform.position) < 5.0f 
        && _aiDestinationSetter != null)
            _aiDestinationSetter.target = _player.transform;
        else if (
            targets.Length > 0 
            && _aiDestinationSetter != null
            && (
                _aiDestinationSetter.target == null 
                || _aiDestinationSetter.target == _player.transform
            ))
        {
            targetIndex = 0;
            _aiDestinationSetter.target = targets[targetIndex].transform;
        }
    }
    public void TargetIsReached()
    {
        Invoke("GoToNextTarget",1.0f);
    }
    public void GoToNextTarget()
    {
        if (_aiDestinationSetter.target != _player.transform)
        {
            targetIndex += 1;
            targetIndex = targetIndex % targets.Length;
            // if (targetIndex == targets.Length) // same thing in two lines
            //     targetIndex = 0;
            _aiDestinationSetter.target = targets[targetIndex].transform;
        }
    }
    
}

using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    [SerializeField] GameObject _player;
    EnemyPath _enemypath;
    public GameObject[] targets = new GameObject[2];
    int targetIndex;
    void Start()
    {
        _enemypath = GetComponent<EnemyPath>();
    }

    void FollowIfClose()
    {
        var dist = Vector2.Distance(
            _player.transform.position, transform.position
            );
        if (dist < 5.0f)
            _enemypath.canMove = true;
        else
            _enemypath.canMove = false;    
    }

    void Update()
    {
        // FollowIfClose();
        if (_player != null 
        && Vector2.Distance(_player.transform.position, transform.position) < 5.0f)
            _enemypath.target = _player.transform;
        else if (
            _player != null
            && targets.Length > 0 
            && (
                _enemypath.target == null 
                || _enemypath.target == _player.transform
            ))
        {
            targetIndex = 0;
            _enemypath.target = targets[targetIndex].transform;
        }
    }
    public void TargetIsReached()
    {
        Debug.Log("Reached");
        Invoke("GoToNextTarget",1.0f);
    }
    public void GoToNextTarget()
    {
        if (_enemypath.target != _player.transform)
        {
            targetIndex += 1;
            targetIndex = targetIndex % targets.Length;
            // if (targetIndex == targets.Length) // same thing in two lines
            //     targetIndex = 0;
            _enemypath.target = targets[targetIndex].transform;
        }
    }
    
}

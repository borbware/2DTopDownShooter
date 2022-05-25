using Pathfinding;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    public bool canMove;
    public Transform target;

    public float pickNextWaypointDist = 1.0f;
    [SerializeField] Vector3 direction;

    Seeker _seeker;
    Path currentPath;
    [SerializeField] int nextWaypoint;
    float speed;
    bool reachedEndOfPath;
    void Start()
    {
        nextWaypoint = 0;
        speed = 3;
        _seeker = GetComponent<Seeker>();
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }
    void UpdatePath()
    {
        if (_seeker.IsDone() && target != null)
            _seeker.StartPath(transform.position, target.position, OnPathCompleted);
    }
    void OnPathCompleted(Path p)
    {
        if (!p.error)
        {
            currentPath = p;
            nextWaypoint = 0;
        }
    }
    void Update()
    {
        if (currentPath == null)
            return;
        
        if (nextWaypoint >= currentPath.vectorPath.Count)
        {
            if (!reachedEndOfPath)
                SendMessage("TargetIsReached");
            reachedEndOfPath = true;
            return;
        } else {
            reachedEndOfPath = false;
        }



        direction = 
            (currentPath.vectorPath[nextWaypoint] - transform.position)
            .normalized;

        if (canMove)
        {
            transform.position += direction * Time.deltaTime * speed;
            transform.up = direction;

            float distance = Vector3.Distance(
                transform.position,
                currentPath.vectorPath[nextWaypoint]
            );
            if (distance < pickNextWaypointDist)
                nextWaypoint++;

        }
        
    }
}

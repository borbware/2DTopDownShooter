using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void ExactFollow()
    {
        transform.position = new Vector3(
            target.transform.position.x,
            target.transform.position.y,
            -10
        );
    }

    void LerpFollow()
    {
        if (target == null)
            return;
        transform.position = Vector3.Lerp(
            transform.position,
            new Vector3(
                target.transform.position.x,
                target.transform.position.y,
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

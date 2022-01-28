using UnityEngine;

public class Scissors : MonoBehaviour
{
    [SerializeField] Transform player;
    private Rigidbody2D rb;
    [SerializeField] float speed = 2;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = player.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        dir.Normalize();
        rb.MovePosition((Vector2)transform.position + ((Vector2)dir * speed * Time.deltaTime));
    }
}

using UnityEngine;

public class AlongWallMovement : MonoBehaviour
{
    public bool isTouchingWall;
    public Vector2 wallNormal;
    private void OnCollisionStay2D(Collision2D other) {
        isTouchingWall = false;
        if (other.gameObject.tag == "Wall")
        {
            isTouchingWall = true;
            Debug.Log("juuh");
            foreach (var item in other.contacts)
            {
                Debug.DrawRay(
                    item.point,
                    item.normal * 100,
                    Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f), 
                    10f
                );
                wallNormal = item.normal;

            }
        }
    }
    private void OnCollisionExit2D() {
        isTouchingWall = false;
        wallNormal = Vector2.zero;
    }
}

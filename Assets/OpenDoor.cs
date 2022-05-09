using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenDoor : MonoBehaviour
{
    public string scene;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "Player")
        {
            SceneManager.LoadScene(scene);
            GameManager.instance.SetPlayerPosition(
                other.gameObject.transform.position
            );
        }
    }
}

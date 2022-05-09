using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int apples = 0;

    public static GameManager instance;
    public Vector3 playerPosition = Vector3.zero;
    GameObject appleUI;
    void Start()
    {

            

        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void AddApples(int newApples)
    {
        apples += newApples;
        apples = Mathf.Max(apples, 0);
        UIManager.instance.UpdateUI();
    }

    public void SetPlayerPosition(Vector3 newPos)
    {
        playerPosition = newPos;
    }

}

using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject apples;
    public static UIManager instance;
    Text appleText;
    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        apples = transform.Find("Apples").gameObject;
        appleText = apples.GetComponent<UnityEngine.UI.Text>();

        UpdateUI();
    }

    public void UpdateUI()
    {
        appleText.text = GameManager.instance.apples.ToString();
    }
}

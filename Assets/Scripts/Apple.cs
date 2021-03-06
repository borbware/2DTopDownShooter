using UnityEngine;

public class Apple : MonoBehaviour
{
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.enabled = true;
    }

    // Update is a function that is called once per frame

    private void OnDestroy() {
        if (audioSource.enabled)
            audioSource.Play();
    }
}

using UnityEngine;

public class Apple : MonoBehaviour
{
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.enabled = true;
    }

    private void OnDestroy() {
        if (audioSource.enabled)
            audioSource.Play();
    }
}

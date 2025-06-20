using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null){
            audioSource.volume = Voleme.volume;
        }
    }

    void Update(){
        if (audioSource != null && audioSource.volume != Voleme.volume){
            audioSource.volume = Voleme.volume;
        }
    }
}

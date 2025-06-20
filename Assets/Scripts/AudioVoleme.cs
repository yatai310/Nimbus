using UnityEngine;

public class AudioVoleme : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.gameObject.GetComponent<AudioSource>().volume = Voleme.volume;
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.GetComponent<AudioSource>().volume = Voleme.volume;        
    }
}

using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource src;
    public AudioClip step;
    public void Step()
    {
        src.clip = step;
        src.Play();
    }
}

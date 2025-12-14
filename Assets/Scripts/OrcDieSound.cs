using UnityEngine;

public class OrcDieSound : MonoBehaviour
{
    public AudioSource src;
    public AudioClip Die;

    public void DieSound()
    {
        src.clip = Die;
        src.Play();
    }
}

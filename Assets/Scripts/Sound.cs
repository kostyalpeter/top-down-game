using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioSource src;
    public AudioClip step, fireball, hit, bow;
    public void Bow()
    {
        src.clip = bow;
        src.Play();
    }
    public void FireBall()
    {
        src.clip = fireball;
        src.Play();
    }
}

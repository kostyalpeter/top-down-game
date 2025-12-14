using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioSource src;
    public AudioClip fireball, hit, bow;
    public void Hitting()
    {
        src.clip = hit;
        src.Play();
    }

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

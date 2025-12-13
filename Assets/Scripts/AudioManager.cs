using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource src;
    public AudioClip step, fireball, hit, bow;
    public void Step()
    {
        src.clip = step;
        src.Play();
    }
    public void Bow()
    {
        src.clip = bow;
        src.Play();
    }
    public void Hit()
    {
        src.clip = hit;
        src.Play();
    }
    public void FireBall()
    {
        src.clip = fireball;
        src.Play();
    }
    public void StepStop();


}

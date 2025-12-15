using UnityEngine;

public class Summon : MonoBehaviour
{
    public GameObject Summoned;
    public float time = 10f;
    public float timer = 0f;
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= time)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }
    public void SpawnEnemy()
    {
        Instantiate(Summoned, transform.position + Vector3.right * 2f, Quaternion.identity);
        Instantiate(Summoned, transform.position + Vector3.left * 2f, Quaternion.identity);
        Instantiate(Summoned, transform.position + Vector3.up * 2f, Quaternion.identity);
        Instantiate(Summoned, transform.position + Vector3.down * 2f, Quaternion.identity);
    }
}

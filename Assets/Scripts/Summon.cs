using UnityEngine;

public class Summon : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] float sizeX = 1f;
    [SerializeField] float sizeY = 1f;
    [SerializeField] float spawnCooldown = 1f;
    [SerializeField] float spawntime;

    void Start()
    {
        spawntime = spawnCooldown;
    }

    private void Update()
    {
        if (spawntime < 0) spawntime -= Time.deltaTime;

        if (spawntime <= 0)
        {
            Instantiate(_enemyPrefab, transform.position, transform.rotation);
            spawntime = spawnCooldown;
        }
    }

    void spawnEnemy()
    {
        float xPos = (Random.value - 0.5f) * 2 * sizeX + gameObject.transform.position.x;
        float yPos = (Random.value - 0.5f) * 2 * sizeY + gameObject.transform.position.x;

        var spawn = Instantiate(_enemyPrefab);

        spawn.transform.position = new Vector3(xPos, yPos, 0);
    }


}

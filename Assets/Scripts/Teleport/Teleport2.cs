using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Teleport2 : MonoBehaviour

{
    [SerializeField] private float interactDistance = 2f;
    private Transform player;
    public float tp;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector2.Distance(transform.position, player.position);

        if (dist <= interactDistance && Input.GetKeyDown(KeyCode.C))
        {
            TeleportPlayer();
        }
    }

    private void TeleportPlayer()
    {
        player.transform.position = new Vector3((float)24.42643, (float)160.3092, 0);

    }
}
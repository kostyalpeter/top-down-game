using System;
using UnityEditor;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [Header("Settings")]
    public GameObject closedDoor;
    public GameObject openDoor1;
    public GameObject openDoor2;
    public float interactDistance = 2f;

    private Transform player;
    private bool opened = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (closedDoor == null)
            closedDoor = gameObject;

        if (openDoor1 != null)
            openDoor1.SetActive(false);
            openDoor2.SetActive(false);
    }

    void Update()
    {
        if (opened || player == null) return;

        float dist = Vector2.Distance(transform.position, player.position);

        if (dist <= interactDistance && Input.GetKeyDown(KeyCode.C))
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        opened = true;

        if (closedDoor != null)
            closedDoor.SetActive(false);

        if (openDoor1 != null)
            openDoor1.SetActive(true);
            openDoor2.SetActive(true);
    }
}

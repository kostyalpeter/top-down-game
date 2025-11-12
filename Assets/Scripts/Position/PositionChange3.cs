using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PositionChange3 : MonoBehaviour

{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("CaveEnter3"))
        {
            transform.position = new Vector3((float)2.93, (float)-316.3313, 0);
        }
    }
}
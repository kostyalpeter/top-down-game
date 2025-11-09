using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PositionChange1 : MonoBehaviour

{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("CaveEnter"))
        {
            transform.position = new Vector3((float)111.9, (float)127.65, 0);
        }
    }
}
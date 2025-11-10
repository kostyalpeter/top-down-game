using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PositionChange2 : MonoBehaviour

{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("CaveEnter2"))
        {
            transform.position = new Vector3((float)111.913, (float)127.502, 0);
        }
    }
}
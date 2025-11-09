using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PositionChange2 : MonoBehaviour

{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("CaveEnter"))
        {
            transform.position = new Vector3((float)-50.68149, (float)-316.3313, 0);
        }
    }
}
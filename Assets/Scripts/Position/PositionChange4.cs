using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PositionChange4 : MonoBehaviour

{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("CaveEnter4"))
        {
            transform.position = new Vector3((float)158.066, (float)127.502, 0);
        }
    }
}
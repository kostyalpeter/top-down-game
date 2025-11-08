using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("CaveEnter"))
        {
            transform.position = new Vector3((float)-51.25, (float)-308.19, 0);
        }
    }
}
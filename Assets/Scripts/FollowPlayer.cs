using UnityEngine;

public class ShieldFollow : MonoBehaviour
{
    public Transform player;
    public Vector2 offset = new Vector2(1f, 0f);
    private SpriteRenderer playerSprite;

    void Start()
    {
        if (player != null)
            playerSprite = player.GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        if (player == null)
            return;

        Vector3 newPos = player.position;

        if (playerSprite != null && playerSprite.flipX)
            newPos += new Vector3(-offset.x, offset.y, 0f);
        else
            newPos += new Vector3(offset.x, offset.y, 0f);

        transform.position = newPos;
    }
}

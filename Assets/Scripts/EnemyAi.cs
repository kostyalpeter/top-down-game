using System.Collections;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    private enum State
    {
        Roaming
    }

    private State state;
    private EnemyPathfinding enemyPathfinding;
    private SpriteRenderer sprite;


    private void Awake()
    {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        state = State.Roaming;
    }

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(RoamingRoutine());
    }

    private IEnumerator RoamingRoutine()
    {
        while (state == State.Roaming)
        {
            Vector2 roamPosition = GetRoamingPosition();
            enemyPathfinding.MoveTo(roamPosition);
            bool facingRight = enemyPathfinding.IsFacingRight();
            if (facingRight)
            {

                sprite.flipX = false;
            }
            else if (!facingRight)
            {

                sprite.flipX = true;
            }
            yield return new WaitForSeconds(2f);
        }
    }

    private Vector2 GetRoamingPosition()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}

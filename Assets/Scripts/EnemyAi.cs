using System.Collections;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    private enum State
    {
        Roaming,
        Chasing,
        Attacking
    }

    private State state;
    private EnemyPathfinding enemyPathfinding;
    private SpriteRenderer sprite;
    private Transform player;
    private Animator animator;
    private bool canAttack = true;

    [Header("AI Settings")]
    public float detectionRange = 5f;
    public float attackRange = 1.2f;
    public float attackCooldown = 1f;
    public LayerMask obstacleMask;
    public LayerMask playerMask;
    public float roamDelay = 2f;

    private void Awake()
    {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        state = State.Roaming;
    }

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(StateMachine());
    }

    private IEnumerator StateMachine()
    {
        while (true)
        {
            switch (state)
            {
                case State.Roaming:
                    yield return StartCoroutine(RoamingRoutine());
                    break;
                case State.Chasing:
                    yield return StartCoroutine(ChasingRoutine());
                    break;
                case State.Attacking:
                    yield return StartCoroutine(AttackRoutine());
                    break;
            }
            yield return null;
        }
    }

    private IEnumerator RoamingRoutine()
    {
        while (state == State.Roaming)
        {
            if (CanSeePlayer())
            {
                state = State.Chasing;
                yield break;
            }

            Vector2 roamTarget = GetRoamingPosition();
            enemyPathfinding.MoveTo(roamTarget);

            sprite.flipX = !enemyPathfinding.IsFacingRight();
            yield return new WaitForSeconds(roamDelay);
        }
    }

    private IEnumerator ChasingRoutine()
    {
        while (state == State.Chasing)
        {
            if (!CanSeePlayer())
            {
                state = State.Roaming;
                yield break;
            }

            float distance = Vector2.Distance(transform.position, player.position);
            if (distance <= attackRange)
            {
                state = State.Attacking;
                yield break;
            }

            enemyPathfinding.MoveTo(player.position);
            sprite.flipX = !enemyPathfinding.IsFacingRight();
            yield return null;
        }
    }

    private IEnumerator AttackRoutine()
    {
        while (state == State.Attacking)
        {
            float distance = Vector2.Distance(transform.position, player.position);

            if (distance > attackRange)
            {
                state = State.Chasing;
                yield break;
            }

            sprite.flipX = (player.position.x < transform.position.x);

            if (canAttack)
            {
                canAttack = false;

                if (animator != null)
                {
                    animator.ResetTrigger("Attack");
                    animator.SetTrigger("Attack");
                }

                enemyPathfinding.StopMoving();

                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                    playerHealth.TakeDamage(1);

                yield return new WaitForSeconds(attackCooldown);

                enemyPathfinding.ResumeMoving();
                canAttack = true;
            }

            yield return null;
        }
    }

    private Vector2 GetRoamingPosition()
    {
        Vector2 randomDir = Random.insideUnitCircle.normalized;
        return (Vector2)transform.position + randomDir;
    }

    private bool CanSeePlayer()
    {
        if (player == null) return false;
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance > detectionRange) return false;

        Vector2 direction = (player.position - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, obstacleMask | playerMask);
        return hit.collider != null && hit.collider.CompareTag("Player");
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
#endif
}

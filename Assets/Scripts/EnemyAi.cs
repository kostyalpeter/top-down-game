using System.Collections;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    private enum State { Roaming, Chasing, Attacking }

    [Header("AI Settings")]
    public float detectionRange = 5f;
    public float attackRange = 1.2f;
    public float attackCooldown = 1f;
    public LayerMask obstacleMask;
    public float roamDelay = 2f;

    [Header("Combat Settings")]
    public int damage = 20;

    private State state;
    private EnemyPathfinding enemyPathfinding;
    private SpriteRenderer sprite;
    private Transform player;
    private Animator animator;
    private bool canAttack = true;

    private PlayerHealth playerHealth;

    private void Awake()
    {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        state = State.Roaming;
    }

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj == null)
        {
            Debug.LogError("[EnemyAi] Nincs Player taggel ellátott objektum a jelenetben.");
            enabled = false;
            return;
        }

        player = playerObj.transform;
        playerHealth = playerObj.GetComponent<PlayerHealth>();

        StartCoroutine(StateMachine());
    }

    private IEnumerator StateMachine()
    {
        while (true)
        {
            switch (state)
            {
                case State.Roaming:   yield return RoamingRoutine(); break;
                case State.Chasing:   yield return ChasingRoutine(); break;
                case State.Attacking: yield return AttackRoutine();  break;
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
            if (sprite) sprite.flipX = !enemyPathfinding.IsFacingRight();

            yield return new WaitForSeconds(roamDelay);
        }
    }

    private IEnumerator ChasingRoutine()
    {
        while (state == State.Chasing)
        {
            if (!CanSeePlayer() || PlayerGoneOrDead())
            {
                enemyPathfinding.StopMoving();
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
            if (sprite) sprite.flipX = (player.position.x < transform.position.x);
            yield return null;
        }
    }

    private IEnumerator AttackRoutine()
    {
        while (state == State.Attacking)
        {
            if (PlayerGoneOrDead())
            {
                enemyPathfinding.StopMoving();
                state = State.Roaming;
                yield break;
            }

            float distance = Vector2.Distance(transform.position, player.position);
            if (distance > attackRange)
            {
                state = State.Chasing;
                yield break;
            }

            if (sprite) sprite.flipX = (player.position.x < transform.position.x);

            if (canAttack)
            {
                canAttack = false;

                if (animator)
                {
                    animator.ResetTrigger("Attack");
                    animator.SetTrigger("Attack");
                }

                enemyPathfinding.StopMoving();

                if (playerHealth != null && !playerHealth.Dead)
                    playerHealth.TakeDamage(damage);

                yield return new WaitForSeconds(attackCooldown);

                enemyPathfinding.ResumeMoving();
                canAttack = true;
            }

            yield return null;
        }
    }

    private bool PlayerGoneOrDead()
    {
        if (player == null) return true;
        if (!player.gameObject.activeInHierarchy) return true;
        if (playerHealth == null) return true;
        return playerHealth.Dead;
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
        int mask = obstacleMask | (1 << player.gameObject.layer);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, mask);

        return hit.collider != null && hit.collider.CompareTag("Player");
    }
}

using System;
using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    [Min(1)] public int maxHealth = 100;
    public int CurrentHealth { get; private set; }
    public bool Dead { get; private set; }

    [Header("Damage i-frames")]
    public float invulnAfterHit = 0.1f;

    private float _lastHitTime = -999f;
    private bool _isInvincible = false;
    private Animator _anim;
    private Rigidbody2D _rb;

    public UnityEvent OnDeath;
    public UnityEvent OnRespawn;

    private static readonly int HitHash = Animator.StringToHash("Hit");
    private static readonly int DieHash = Animator.StringToHash("Die");

    void Awake()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        CurrentHealth = Mathf.Max(1, maxHealth);
        Dead = false;
    }

    public void TakeDamage(int amount)
    {
        if (Dead) return;
         if (invincible) return;
        if (Time.time - _lastHitTime < invulnAfterHit) return;
        _lastHitTime = Time.time;

        amount = Mathf.Max(0, amount);
        CurrentHealth = Mathf.Clamp(CurrentHealth - amount, 0, maxHealth);

        var attack = GetComponent<PlayerAttack>();
        if (attack != null)
        {
            attack.SetTakingDamage(true);
            StartCoroutine(ResetDamageLock(attack));
        }

        if (CurrentHealth > 0)
        {
            if (_anim) _anim.SetTrigger(HitHash);
        }
        else
        {
            Die();
        }
    }

    private IEnumerator ResetDamageLock(PlayerAttack attack)
    {
        yield return new WaitForSeconds(0.6f);
        if (attack != null)
            attack.SetTakingDamage(false);
    }

    void Die()
    {
        if (Dead) return;
        Dead = true;

        if (_anim)
        {
            _anim.ResetTrigger(HitHash);
            _anim.SetTrigger(DieHash);
        }


        if (_rb)
        {
            _rb.linearVelocity = Vector2.zero;
            _rb.angularVelocity = 0f;
            _rb.isKinematic = true;
        }

        var cols = GetComponentsInChildren<Collider2D>(true);
        foreach (var c in cols) c.enabled = false;

        var behaviours = GetComponentsInChildren<MonoBehaviour>(true);
        foreach (var mb in behaviours)
        {
            if (mb == this) continue;
            mb.enabled = false;
        }

        LivesCounter livesCounter = FindObjectOfType<LivesCounter>();
        bool isGameOver = false;

        if (livesCounter != null)
        {
            livesCounter.Removelife(1);

            int? remaining = TryGetRemainingLives(livesCounter);
            if (remaining.HasValue && remaining.Value <= 0)
            {
                isGameOver = true;
            }
        }

        if (isGameOver)
        {
            if (OnDeath != null)
                OnDeath.Invoke();
            return;
        }

        StartCoroutine(RespawnAfterDelay());
    }

    private int? TryGetRemainingLives(object livesCounter)
    {
        if (livesCounter == null) return null;
        Type t = livesCounter.GetType();

        string[] propNames = { "Lives", "CurrentLives", "currentLives", "remainingLives", "RemainingLives", "lives" };
        foreach (var name in propNames)
        {
            PropertyInfo p = t.GetProperty(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (p != null && p.PropertyType == typeof(int))
                return (int)p.GetValue(livesCounter);
        }

        string[] fieldNames = { "Lives", "lives", "currentLives", "remainingLives" };
        foreach (var name in fieldNames)
        {
            FieldInfo f = t.GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (f != null && f.FieldType == typeof(int))
                return (int)f.GetValue(livesCounter);
        }

        string[] methodNames = { "GetLives", "GetRemainingLives", "RemainingLives", "GetCurrentLives" };
        foreach (var name in methodNames)
        {
            MethodInfo m = t.GetMethod(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
            if (m != null && m.ReturnType == typeof(int))
                return (int)m.Invoke(livesCounter, null);
        }

        return null;
    }

    private IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(5f);

        // Reset health and state
        CurrentHealth = Mathf.Max(1, maxHealth);
        Dead = false;

        // Reset animator
        if (_anim)
        {
            _anim.Rebind();
            _anim.Update(0f);
            _anim.Play("Idle", 0, 0f);
            _anim.ResetTrigger(DieHash);
            _anim.ResetTrigger(HitHash);
        }

        // Reset physics
        if (_rb)
        {
            _rb.isKinematic = false;
            _rb.linearVelocity = Vector2.zero;
            _rb.angularVelocity = 0f;
        }

        // Re-enable colliders and behaviors
        var cols = GetComponentsInChildren<Collider2D>(true);
        foreach (var c in cols) c.enabled = true;

        var behaviours = GetComponentsInChildren<MonoBehaviour>(true);
        foreach (var mb in behaviours)
        {
            if (mb == this) continue;
            mb.enabled = true;
        }

        // Add 2 seconds of invincibility
        invincible = true;
        StartCoroutine(DisableInvincibilityAfterDelay(2f));

        if (OnRespawn != null)
            OnRespawn.Invoke();
    }

    private IEnumerator DisableInvincibilityAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        invincible = false;
    }

    private bool invincible = false;

    public void SetInvincibleState(bool state)
    {
        invincible = state;
    }

    public void ApplyDamage(int amount)
    {
        if (Dead) return;
        if (invincible) return;
        if (Time.time - _lastHitTime < invulnAfterHit) return;

        _lastHitTime = Time.time;
        amount = Mathf.Max(0, amount);
        CurrentHealth = Mathf.Clamp(CurrentHealth - amount, 0, maxHealth);

        if (CurrentHealth > 0)
        {
            if (_anim) _anim.SetTrigger("Hit");
        }
        else
        {
            Die();
        }
    }

    internal void Heal(int healAmount)
    {
        if (Dead) return;

        healAmount = Mathf.Max(0, healAmount);
        CurrentHealth = Mathf.Clamp(CurrentHealth + healAmount, 0, maxHealth);
    }
}

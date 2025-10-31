using System;
using UnityEngine;

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
        if (_isInvincible) return;
        if (Time.time - _lastHitTime < invulnAfterHit) return;

        _lastHitTime = Time.time;
        amount = Mathf.Max(0, amount);
        CurrentHealth = Mathf.Clamp(CurrentHealth - amount, 0, maxHealth);

        if (CurrentHealth > 0)
        {
            if (_anim) _anim.SetTrigger(HitHash);
        }
        else
        {
            Die();
        }
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

        if (GameManager.instance != null)
            GameManager.instance.TriggerGameOver();

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
    }

    public void SetInvincible(bool state)
    {
        _isInvincible = state;
    }
}

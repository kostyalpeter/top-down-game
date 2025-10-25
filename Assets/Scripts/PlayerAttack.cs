using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        if (animator == null)
        {
            Debug.LogError("Animator component is not assigned!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key pressed"); // Debug log
            animator.SetTrigger("Attack");
        }
    }
}
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetSpeed(float speed)
    {
        animator.SetFloat("Speed", speed);
    }

    public void TriggerJump()
    {
        animator.SetTrigger("Jump");
    }

    public void TriggerAttack()
    {
        animator.SetTrigger("Attack");
    }
}

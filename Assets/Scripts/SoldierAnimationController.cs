using UnityEngine;

public class SoldierAnimationController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>(); // Prefab'daki Animator bile�enini al
    }

    // Y�r�me animasyonunu ba�lat�r
    public void SetWalk(bool walking)
    {
        animator.SetBool("isWalking", walking);
    }

    // Sald�r� animasyonunu ba�lat�r
    public void SetAttack(bool attacking)
    {
        animator.SetBool("isAttacking", attacking);
    }

    // Idle'a d�ner (y�r�meyi veya sald�r�y� sonland�r�r)
    public void SetIdle()
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("isAttacking", false);
    }
}

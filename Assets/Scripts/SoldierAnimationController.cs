using UnityEngine;

public class SoldierAnimationController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>(); // Prefab'daki Animator bileþenini al
    }

    // Yürüme animasyonunu baþlatýr
    public void SetWalk(bool walking)
    {
        animator.SetBool("isWalking", walking);
    }

    // Saldýrý animasyonunu baþlatýr
    public void SetAttack(bool attacking)
    {
        animator.SetBool("isAttacking", attacking);
    }

    // Idle'a döner (yürümeyi veya saldýrýyý sonlandýrýr)
    public void SetIdle()
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("isAttacking", false);
    }
}

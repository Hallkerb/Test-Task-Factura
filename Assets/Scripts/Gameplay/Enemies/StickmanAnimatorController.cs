using UnityEngine;

public class StickmanAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void StartMoveAnimation()
    {
        animator.Play("StartRunning");
    }

    public void ResetState()
    {
        animator.Rebind();
    }
}

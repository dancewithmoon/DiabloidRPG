using UnityEngine;

public enum AnimationStates
{
    Idle,
    Run,
    Die
}


[RequireComponent(typeof(Animator))]
public class CharacterAnimator : MonoBehaviour
{
    private Animator animator;

    public void Move()
    {
        animator.Play(AnimationStates.Run.ToString());
    }

    public void Stay()
    {
        animator.Play(AnimationStates.Idle.ToString());
    }

    public void Die()
    {
        animator.Play(AnimationStates.Die.ToString());
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        Stay();
    }
}

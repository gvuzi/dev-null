using UnityEngine;

public class PlayerAnimationChanger : MonoBehaviour
{
    public Animator animator;
    public string currentState;

  
    public void ChangeAnimationState(string newAnimationState, float crossFade = 0.4f) {
        if (currentState == newAnimationState) {
            return;
        }
        animator.CrossFade(newAnimationState, crossFade);
        currentState = newAnimationState;
    }
}

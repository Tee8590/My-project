using UnityEngine;

public class Animations : MonoBehaviour
{
    public Animator animator;
   
    public void IdleAnim()
    {
        animator.SetTrigger("Idle");
    }
    public void H1Anim()
    {
        animator.SetTrigger("H1Trigger");
    }public void H2Anim()
    {
        animator.SetTrigger("H2Trigger");
    }
    //public enum AnimationState
    //{
    //    Idle,
    //    H1,
    //    H2
    //}
    //private AnimationState animState;
    //void Start()
    //{
    //     animState = AnimationState.Idle;
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    switch (animState)
    //    {
    //        case AnimationState.Idle:
                
    //            break;
    //    }
    //}
}

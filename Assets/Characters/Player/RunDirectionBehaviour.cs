using UnityEngine;
using System.Collections;

public class RunDirectionBehaviour : StateMachineBehaviour {
    
    private int xVelocityParam = Animator.StringToHash("X-Velocity");
    private Vector3 rightDirection = new Vector3(0, 90, 0);
    private Vector3 leftDirection = new Vector3(0, -90, 0);
    private Transform tx;
    private Vector3 currentDirection = Vector3.zero;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        tx = animator.GetComponent<Transform>();
        currentDirection = Vector3.zero;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (animator.GetFloat(xVelocityParam) > 0 && currentDirection != rightDirection)
        {
            tx.eulerAngles = rightDirection;
            currentDirection = rightDirection;
        }
        else if (animator.GetFloat(xVelocityParam) < 0 && currentDirection != leftDirection)
        {
            tx.eulerAngles = leftDirection;
            currentDirection = leftDirection;
        }
	
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        currentDirection = Vector3.zero;
    }

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}

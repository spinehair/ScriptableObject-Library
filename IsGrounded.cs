using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    [CreateAssetMenu(menuName = "Actions/State Actions/IsGrounded")]
    public class IsGrounded : StateActions
    {
        public TierOneRaycast raycast;

        public override void Execute(StateManager states)
        {
            raycast.originOffset = Vector3.up * 0.7f;
            raycast.origin += raycast.originOffset;
            raycast.Execute(states);

            if(raycast.Hit)       
                states.isGrounded = true;
            
            else
                states.isGrounded = false;
            

            states.anim.SetBool(states.hashes.IsGrounded, states.isGrounded);
        }

    }
}



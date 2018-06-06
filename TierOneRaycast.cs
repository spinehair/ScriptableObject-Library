using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA {
    [CreateAssetMenu(menuName = "CastAction/T1 Cast")]
    public class TierOneRaycast : CastAction {

        [Header("RaySpawning")]
        public float RayDistance = 1;

        public NamedVectorDirection direction;
        public PreciseVectorDirection preciseDirection;

        public bool isTriggerQuery = false;
        public LayerMask hitLayers;


        public Vector3 Direction { get; set; }
        public Vector3 originOffset { get; set; }
        public Vector3 origin { get; set; }

        public RaycastData hitData { get; private set; }

        [Header("Debug")]
        public bool DebugDraw = true;
        public Color DrawColor;

        public bool Hit { get; private set; }
        public bool IsSlave { get; set; }


        public override void Execute(StateManager states)
        {
            Direction = m_Direction(direction,preciseDirection,states.mTransform);

            if (Direction == Vector3.zero)
                return;

            if (origin != (states.mTransform.position + originOffset) && !IsSlave)
                origin = states.mTransform.position + originOffset;

            Hit = isHit(states.masterHitData);

            if (!Hit && !hitData.Cleared)
            {
                hitData.ClearData();
                states.interactionData.ClearData();
                return;
            }


            if (!DebugDraw)
                return;

            Debug.DrawRay(origin, Direction * RayDistance, DrawColor);

            
        }

        public bool isHit(EasyCastMasterData masterHitGather)
        {
            
            RaycastHit hit;

            if(Physics.Raycast(origin, Direction, out hit, RayDistance, hitLayers, isTriggerQuery ? QueryTriggerInteraction.Collide:QueryTriggerInteraction.Ignore))
            {
                hitData.SetData(hit.point, hit.normal);

                if (hit.collider)
                {
                    hitData.SetCollider(hit.collider);
                }

             
                return true;
            }
            else
            {

                
            }


            return false;


        }

        Vector3 m_Direction(NamedVectorDirection nD, PreciseVectorDirection pD, Transform casterTransform)
        {
            Vector3 result = Vector3.zero;

            switch (nD)
            {
                case NamedVectorDirection.Forwards:
                    result = casterTransform.forward;
                    break;
                case NamedVectorDirection.Backwards:
                    result = -casterTransform.forward;
                    break;
                case NamedVectorDirection.Upwards:
                    result = casterTransform.up;
                    break;
                case NamedVectorDirection.Downwards:
                    result = -casterTransform.up;
                    break;
                case NamedVectorDirection.Right:
                    result = casterTransform.right;
                    break;
                case NamedVectorDirection.Left:
                    result = -casterTransform.right;
                    break;
                default:
                    break;
            }

            if (pD == PreciseVectorDirection.NotApplied)
                return result;

            switch (pD)
            {            
                case PreciseVectorDirection.Acute_Lateral:
                    result += new Vector3( .5f, 0, 0);
                    break;
                case PreciseVectorDirection.Obtuse_Lateral:
                    result += new Vector3( -.5f, 0, 0);
                    break;
                case PreciseVectorDirection.Acute_Upwards:
                    result += new Vector3( .5f, .5f, 0);
                    break;
                case PreciseVectorDirection.Obtuse_Upwards:
                    result += new Vector3( -.5f, .5f, 0);
                    break;
                case PreciseVectorDirection.Acute_Downwards:
                    result += new Vector3( .5f, -.5f, 0);
                    break;
                case PreciseVectorDirection.Obtuse_Downwards:
                    result += new Vector3( -.5f, -.5f, 0);
                    break;
                default:
                    break;
            }

            return result;

        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public enum NamedVectorDirection { Forwards, Backwards, Upwards, Downwards, Right, Left, Zero}//Zero Added for overriding
                                           //pitch,yaw
    public enum PreciseVectorDirection { NotApplied,Acute_Lateral, Obtuse_Lateral,
                                                      Acute_Upwards, Obtuse_Upwards,
                                                        Acute_Downwards, Obtuse_Downwards}

    [System.Serializable]
    public class RaycastData
    {

        public Vector3 hitNormal;
        public Vector3 hitPoint;
        public Collider hitCollider;


        public bool Cleared { get; private set; }

        public void SetData(Vector3 point, Vector3 normal)
        { 
            hitNormal = normal;
            hitPoint = point;



            Cleared = false;
        }

        public void SetCollider(Collider target)
        {

            hitCollider = target;

        }
        public void ClearData()
        {
            hitNormal = Vector3.zero;
            hitPoint = Vector3.zero;

            Cleared = true;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public abstract class CastAction : ScriptableObject
    {

        public abstract void Execute(StateManager states);
        //New action i implement to handle Raycast Data. Instead of creating raycasts with the same direction but different distances, we will use these actions to recycle raycasts amoungt different actions.

    }
}
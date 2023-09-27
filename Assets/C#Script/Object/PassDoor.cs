using System;
using C_Script.Common;
using C_Script.Common.Animation;
using UnityEngine;

namespace C_Script.Object
{
    [RequireComponent(typeof(AnimationEventTrigger))]
    public class PassDoor : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if(col.CompareTag("Player"))
                GetComponent<AnimationEventTrigger>().AnimatorSetTrigger();
        }
    }
}

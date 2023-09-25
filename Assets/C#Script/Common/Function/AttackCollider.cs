using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace C_Script.Common.Function
{
    public class AttackCollider : MonoBehaviour
    {
        [NonSerialized]public bool InAttackRange;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                InAttackRange = true;
            }
        }
        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                InAttackRange = false;
            }
        }
    }
}

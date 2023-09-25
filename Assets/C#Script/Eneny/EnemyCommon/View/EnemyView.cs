using System;
using C_Script.Common.Model.EventCentre;
using C_Script.Manager;
using UnityEngine;
using UnityEngine.Events;

namespace C_Script.Eneny.EnemyCommon.View
{
    public class EnemyView : BaseClass.View
    {
        //暴击
        [NonSerialized]public UnityEvent EnemyHurtCrit = new ();
        //不暴击
        [NonSerialized] public UnityEvent EnemyHurtNoCrit = new();

        [NonSerialized]public UnityEvent EnemyDeath = new ();


        private void OnEnable()
        {
            EnemyDeath.AddListener(EnemyReduce);
        }

        private void OnDisable()
        {
            EnemyDeath.RemoveListener(EnemyReduce);
        }

        public void EnemyReduce()
        {
            CombatEventCentreManager.Instance.Publish(CombatEventType.EnemyNumChange);
        }
    }
}
using C_Script.Common.Model.ObjectPool;
using C_Script.Eneny.Boss.DemonBoss.BaseClass;
using C_Script.Eneny.Boss.DemonBoss.State.StateBase;
using UnityEngine;

namespace C_Script.Eneny.Boss.DemonBoss.State
{
    public class WaitStateDemonBoss:DemonBossState
    {
        public override void Enter()
        {
            Owner.Factory.effect1.SetActive(false);
            if (BigObjectPool.Instance.HasKey(ObjectType.Skull))
            {
                BigObjectPool.Instance.SetAllFalse(ObjectType.Skull);
            }
        }
        
        public override void PhysicExcute()
        {
            
        }
        
        public override void LogicExcute()
        {
            
        }

        public override void Exit()
        {
            
        }

        public WaitStateDemonBoss(DemonBossBase owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
        {
        }
    }
}
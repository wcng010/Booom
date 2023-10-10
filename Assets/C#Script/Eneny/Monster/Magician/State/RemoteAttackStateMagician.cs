using System;
using System.Collections;
using C_Script.BaseClass;
using C_Script.Common.Model.ObjectPool;
using C_Script.Eneny.Monster.Magician.BaseClass;
using C_Script.Eneny.Monster.Magician.Component;
using C_Script.Eneny.Monster.Magician.State.StateBase;
using C_Script.Object;
using UnityEngine;

namespace C_Script.Eneny.Monster.Magician.State
{
    public class RemoteAttackStateMagician:MagicianState
    {
        private int _counter;
        private GameObject _meteorite;
        private Vector2 _toTargetVector2;
        private bool _canChange;

        // ReSharper disable Unity.PerformanceAnalysis
        public override void Enter()
        {
            base.Enter();
            _counter++;
            Owner.StartCoroutine(RemoteAttackBehaviour());
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        public override void PhysicExcute()
        {
            
        }

        public override void LogicExcute()
        {
            SwitchState();
        }
        
        IEnumerator RemoteAttackBehaviour()
        {
            int tag = _counter;
            while (true)
            {
                if ((Mathf.Abs(_toTargetVector2.x) < MagicianData.RemoteAttackRange
                    &&(Mathf.Abs(_toTargetVector2.x)>MagicianData.MeleeAttackRange)))
                {
                    if (!_meteorite||_meteorite.activeSelf == false)
                    {
                        _canChange = false;
                        yield return new WaitForSeconds(MagicianData.RemoteAttackCoolDown);
                        if(StateMachine.CurrentState!=this||tag!=_counter) yield break;
                        _meteorite = BigObjectPool.Instance.SetOneActive(ObjectType.Meteorite);
                        _meteorite.GetComponent<Meteorite>().FireMeteorite
                            (new Vector3(TransformOwner.transform.position.x,TransformOwner.transform.position.y+2f,TransformOwner.transform.position.z));
                        _canChange = true;
                    }
                }
                yield return new WaitForSeconds(0.05f);
                if(StateMachine.CurrentState!=this)
                    yield break;
            }
        }
        
        public RemoteAttackStateMagician(MagicianBase owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
        {
            BigObjectPool.Instance.PushEmptyPool(ObjectType.Meteorite,MagicianData.Meteorite);
        }
        private void SwitchState()
        {
            _toTargetVector2 = TargetTrans.position-TransformOwner.position;
            float toTargetDis = MathF.Abs(_toTargetVector2.magnitude);
            TransformOwner.localScale = new Vector3(_toTargetVector2.x/Mathf.Abs(_toTargetVector2.x), 1, 1);
            //距离大于远程攻击范围
            if (toTargetDis > MagicianData.RemoteAttackRange&&_canChange)
            {
                StateMachine.ChangeState(MagicianDic[EnemyStateType.PursuitStateEnemy]);
            }
            //距离小于近战攻击范围
            else if (toTargetDis < MagicianData.MeleeAttackRange&&_canChange)
            {
                StateMachine.ChangeState(MagicianDic[EnemyStateType.MeleeAttackStateEnemy]);
            }
        }
    }
}
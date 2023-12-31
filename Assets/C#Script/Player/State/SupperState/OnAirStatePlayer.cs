﻿using System.Collections;
using C_Script.Common.Model.ObjectPool;
using C_Script.Player.Base;
using C_Script.Player.Component;
using C_Script.Player.MVC.Model;
using C_Script.Player.State.BaseState;
using UnityEngine;

namespace C_Script.Player.StateModel.SupperState
{
    public class OnAirStatePlayer : PlayerState
    {
        // ReSharper disable Unity.PerformanceAnalysis
        private bool _isBigFall;
        private string _fallAsh;
        private string _bigFallAsh;

        public override void Enter()
        {
            base.Enter();
            _isBigFall = false;
        }

        public override void PhysicExcute()
        {
            FallBahaviour();
        }

        public override void LogicExcute()
        {
            SwitchState();
        }

        public override void Exit()
        {
            base.Exit();
        }
        
        protected void FallBahaviour()=> MoveBehaviour(PlayerData.MaxSpeedX,PlayerData.AccelerationX);
        
        private void SwitchState()
        {
            if (KKey&& SkillData.commonSkills["BigFall"])
            {
                _isBigFall = true;
                //加速下落
                Rigidbody2DOwner.velocity += new Vector2(0, -1);
            }
            
            if (Owner.IsGroundOneRay&&!_isBigFall)
            {
                PlayerModel.PlayerAudioTrigger.LandPlay();
                AshObjectPool.Instance.SetActive(_fallAsh);
                StateMachine.RevertOrinalState();
            }
            else if (Owner.IsGroundOneRay&&_isBigFall)
            {
                PlayerModel.PlayerAudioTrigger.LandPlay();
                AshObjectPool.Instance.SetActive(_bigFallAsh);
                StateMachine.ChangeState(StateDictionary[PlayerStateType.RollStatePlayer]);
            }
            
            RaycastHit2D headFrontHit2D= Owner.HeadFrontHit2D;
            RaycastHit2D bodyFrontHit2D = Owner.BodyFrontHit2D;
            if (!bodyFrontHit2D&&headFrontHit2D)
            {
                HandRevision(headFrontHit2D.point);
                StateMachine.ChangeState(Owner.PlayerStateDic[PlayerStateType.HangingStatePlayer]);
            }
            if(JKey&& PressJKeyCount==0)
                StateMachine.ChangeState(Owner.PlayerStateDic[PlayerStateType.AttackState1Player]);
            if(QKey&&SkillData.commonSkills["Dash"])
                StateMachine.ChangeState(Owner.PlayerStateDic[PlayerStateType.DashStatePlayer]);
        }
        
        private void HandRevision(Vector2 hitPoint)
        {
            Vector2 handPoint = OwnerCore.GetCoreComponent<CollisionComponent>().HandTrans.position;
            Vector2 revisionPoint = hitPoint + (Vector2)TransformOwner.position - handPoint;
            TransformOwner.position = revisionPoint;
        }
        

        public OnAirStatePlayer(PlayerBase owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
        {
            AshObjectPool.Instance.PushObject(GameObject.Instantiate( PlayerData.FallAsh, AshObjectPool.Instance.transform));
            AshObjectPool.Instance.PushObject(GameObject.Instantiate(PlayerData.BigFallAsh,AshObjectPool.Instance.transform));
            _fallAsh = PlayerData.FallAsh.name;
            _bigFallAsh = PlayerData.BigFallAsh.name;
        }
    }
}
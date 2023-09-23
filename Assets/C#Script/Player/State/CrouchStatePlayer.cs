using System.Numerics;
using C_Script.Player.Base;
using C_Script.Player.State.BaseState;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace C_Script.Player.StateModel
{
    public class CrouchStatePlayer : PlayerState
    {
        private static readonly int Crouch = Animator.StringToHash("Crouch");
        
        public override void Enter()
        {
            base.Enter();
            ChangeColliderYSize(Collider2DOwner,PlayerData.CrouchSizeY);
            Rigidbody2DOwner.velocity = Vector2.zero;
        }
        public override void LogicExcute()
        {
            SwitchState();
        }
        public override void Exit()
        {
            base.Exit();
            ChangeColliderYSize(Collider2DOwner,PlayerData.IdleSizeY);
        }
        private void SwitchState()
        {
            //Return OnGroundState
            if (YAxis >= 0)
                StateMachine.ChangeState(StateDictionary[PlayerStateType.OnGroundStatePlayer]);
            //CrouchMove
            else if (XAxis != 0)
                StateMachine.ChangeState(StateDictionary[PlayerStateType.CrouchMoveStatePlayer]);
            //CrouchAttack
            else if(JKey)
                StateMachine.ChangeState(StateDictionary[PlayerStateType.CrouchAttackStatePlayer]);
            //SlideState
            else if(KKey)
                StateMachine.ChangeState(StateDictionary[PlayerStateType.SlideStatePlayer]);
        }

        public CrouchStatePlayer(PlayerBase owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
        {

        }
    }
}

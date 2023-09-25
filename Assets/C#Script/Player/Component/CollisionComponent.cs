using System;
using C_Script.BaseClass;
using Sirenix.OdinInspector;
using UnityEngine;

namespace C_Script.Player.Component
{
    public class CollisionComponent : CoreComponent
    {
        #region PosSetting
        [field:FoldoutGroup("CommonSetting")] [field:SerializeField] public Transform BodyTrans { get; private set; }
        [field:FoldoutGroup("CommonSetting")] [field:SerializeField] public Transform HeadTrans { get; private set; }
        [field:FoldoutGroup("CommonSetting")] [field: SerializeField] public Transform FootTrans { get; private set; }
        [field:FoldoutGroup("CommonSetting")] [field: SerializeField] public Transform LowerRightPoint { get; private set;}
        [field:FoldoutGroup("CommonSetting")] [field: SerializeField] public Transform LowerLeftPoint { get; private set; }
        [field:FoldoutGroup("CommonSetting")][field: SerializeField] public Transform HandTrans{ get; private set; }
        [field:FoldoutGroup("PlayerUse")] [field: SerializeField] public float GroundCheckDistance { get; private set; }
        [field:FoldoutGroup("PlayerUse")] [field: SerializeField] public float WallCheckDistance { get; private set; }
        [field: FoldoutGroup("EnemyUse")] [field: SerializeField] public float AttackCheckDistance { get; private set; }

        [field: SerializeField] public LayerMask CheckLayer { get; private set; }
        
        [field: SerializeField] public Transform OwnerTrans { get; private set; }
        
        [field: SerializeField] public bool IsDebug { get; private set; }
        
        #endregion

        #region PlayerCheck
        public bool BodyFront {
            get
            {
                if(IsDebug)
                    Debug.DrawLine(BodyTrans.position,
                        BodyTrans.position + Vector3.right * (OwnerTrans.localScale.x * WallCheckDistance), Color.red);
                return Physics2D.Raycast(BodyTrans.position, Vector2.right * OwnerTrans.localScale.x, WallCheckDistance,
                    CheckLayer);
            }
        }

        public RaycastHit2D BodyFrontHit
        {
            get => Physics2D.Raycast(BodyTrans.position, Vector2.right * OwnerTrans.localScale.x, WallCheckDistance,
                CheckLayer);
        }
        public bool HeadFront {
            get
            {   if(IsDebug)
                     Debug.DrawLine(HeadTrans.position,
                          HeadTrans.position+Vector3.right * (OwnerTrans.localScale.x * WallCheckDistance),Color.red);
                return Physics2D.Raycast(HeadTrans.position, Vector2.right * OwnerTrans.localScale.x,
                    WallCheckDistance, CheckLayer);
            }
        }
        public RaycastHit2D HeadFrontHit {
            get => Physics2D.Raycast(HeadTrans.position, Vector2.right * OwnerTrans.localScale.x, WallCheckDistance,
                CheckLayer);
        }
        public bool FootFront {
            get
            {   if(IsDebug)
                Debug.DrawLine(FootTrans.position,
                    FootTrans.position+Vector3.right * (OwnerTrans.localScale.x * WallCheckDistance),Color.red);
                return Physics2D.Raycast(FootTrans.position, Vector2.right * OwnerTrans.localScale.x,
                    WallCheckDistance, CheckLayer);
            }
        }
        public bool DashDistanceCheck(float dashDistance)
        {
            if(IsDebug)
                Debug.DrawLine(BodyTrans.position,new Vector2(BodyTrans.position.x+dashDistance,BodyTrans.position.y),Color.green);
            return Physics2D.Raycast(BodyTrans.position, Vector2.right * OwnerTrans.localScale.x, dashDistance, CheckLayer);
        }
        public bool RayGroundCheck
        {
            get => Physics2D.Raycast(FootTrans.position, Vector2.down, GroundCheckDistance / 2, CheckLayer);
        }
        public bool ThreeRaysGroundCheck{
            get =>Physics2D.Raycast(FootTrans.position, Vector2.down, GroundCheckDistance / 2, CheckLayer) ||
                       Physics2D.Raycast(LowerLeftPoint.position, Vector2.down, GroundCheckDistance / 2, CheckLayer) ||
                       Physics2D.Raycast(LowerRightPoint.position, Vector2.down, GroundCheckDistance / 2, CheckLayer);
        }
        #endregion

        #region EnemyCheck
        public RaycastHit2D EnemyFrontCheck {
            get
            {
                var headBool = Physics2D.Raycast(HeadTrans.position, Vector2.left * OwnerTrans.localScale.x,
                    AttackCheckDistance,
                    CheckLayer);
                
                var bodyBool = Physics2D.Raycast(BodyTrans.position, Vector2.left * OwnerTrans.localScale.x,
                    AttackCheckDistance,
                    CheckLayer);
                
                var footBool = Physics2D.Raycast(FootTrans.position, Vector2.left * OwnerTrans.localScale.x,
                    AttackCheckDistance,
                    CheckLayer);
                if (headBool.collider)
                {
                    return headBool;
                }
                if (bodyBool.collider)
                {
                    return bodyBool;
                }
                return footBool;
            }
        }
        #endregion

        private void OnDrawGizmos()
        {
            if (IsDebug)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(HeadTrans.position,
                    HeadTrans.position + Vector3.left * OwnerTrans.localScale.x * AttackCheckDistance);
                Gizmos.DrawLine(BodyTrans.position,
                    BodyTrans.position + Vector3.left * OwnerTrans.localScale.x * AttackCheckDistance);
                Gizmos.DrawLine(FootTrans.position,
                    FootTrans.position + Vector3.left * OwnerTrans.localScale.x * AttackCheckDistance);
            }
        }
    }
}

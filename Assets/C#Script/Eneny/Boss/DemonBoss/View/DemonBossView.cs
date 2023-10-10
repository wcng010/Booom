using C_Script.Common.Model.ObjectPool;
using C_Script.Eneny.EnemyCommon.View;
using UnityEngine;

namespace C_Script.Eneny.Boss.DemonBoss.View
{
    public class DemonBossView : EnemyView
    {
        public override void AfterEnemyDeath()
        {
            BigObjectPool.Instance.SetAllFalse(ObjectType.Skull);
            BigObjectPool.Instance.SetAllFalse(ObjectType.PurpleStar);
        }
    }
}
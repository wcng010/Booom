using C_Script.BaseClass;
using C_Script.Common.Model.EventCentre;
using C_Script.Common.Model.ObjectPool;
using C_Script.Eneny.EnemyCommon.Model;
using C_Script.Eneny.EnemyCommon.View;
using C_Script.Manager;
using UnityEngine;

namespace C_Script.Eneny.EnemyCommon.Component
{
    public class EnemyHealth : CoreComponent
    {
        //private EnemyData _enemyData;
        private EnemyModel EnemyModel => Model as EnemyModel;
        private Transform TargetTrans => EnemyModel.TargetTrans;
        private EnemyView EnemyView => View as EnemyView;
        private Rigidbody2D Rb =>  EnemyModel.EnemyRigidbody2D;
        private Transform _mytrans;
        private EnemyData EnemyData => EnemyModel.EnemyData;
        private GameObject HitEffect1 => EnemyData.HitEffect1;
        private GameObject HitEffect2 => EnemyData.HitEffect2;
        private GameObject HitEffect3 => EnemyData.HitEffect3;
        protected override void Awake()
        {
            base.Awake();
            BigObjectPool.Instance.PushEmptyPool(ObjectType.HitEffect1,HitEffect1);
            BigObjectPool.Instance.PushEmptyPool(ObjectType.HitEffect2,HitEffect2);
            BigObjectPool.Instance.PushEmptyPool(ObjectType.HitEffect3,HitEffect3);
            _mytrans = EnemyModel.EnemyTrans;
        }

        protected virtual void Start() => InitHealth();
        protected virtual void InitHealth() { 
            EnemyData.CurrentHealth = EnemyData.MaxHealth;
        }
        public void EnemyDamageWithoutPower(float amount)
        {
            if (EnemyData.AttackInvalid) return;
            CommonDamage(amount);
            EnemyView.EnemyHurtNoCrit.Invoke();
            var effectTrans = BigObjectPool.Instance.SetOneActive(ObjectType.HitEffect1).transform;
            effectTrans.position = _mytrans.position + _mytrans.lossyScale.x*(Vector3)EnemyData.HitEffectOffSet1;
            effectTrans.SetParent(_mytrans);
        }
        public void EnemyDamageWithPower(float amount, Vector2 forceVector2,bool isCritical = false)
        {
            if (EnemyData.AttackInvalid) return;
            CommonDamage(amount);
            if (isCritical)
            {
                CombatEventCentreManager.Instance.Publish(CombatEventType.CriticalStrike);
                AudioManager.Instance.PlayerCriticalAttackPlay();
                var effectTrans = BigObjectPool.Instance.SetOneActive(ObjectType.HitEffect3).transform;
                effectTrans.position = _mytrans.position - _mytrans.lossyScale.x*(Vector3)EnemyData.HitEffectOffSet3;
                effectTrans.localScale = new Vector3(TargetTrans.position.x - _mytrans.position.x > 0?1:-1 ,1,1);
                effectTrans.SetParent(_mytrans);
                EnemyView.EnemyHurtCrit.Invoke();
            }
            else
            {
                var effectTrans = BigObjectPool.Instance.SetOneActive(ObjectType.HitEffect2).transform;
                effectTrans.position = _mytrans.position - _mytrans.lossyScale.x*(Vector3)EnemyData.HitEffectOffSet2;
                effectTrans.localScale = new Vector3(TargetTrans.position.x - _mytrans.position.x > 0?1:-1 ,1,1);
                effectTrans.SetParent(_mytrans);
                EnemyView.EnemyHurtNoCrit.Invoke();
            }
            Rb.velocity = Vector2.zero;
            Rb.AddForce(new Vector2(forceVector2.x,0).normalized*EnemyData.HitForceForward,ForceMode2D.Impulse);
        }

        public void EnemyDamageWithSkill(float amount, Vector2 forceVector2)
        {
            if (EnemyData.AttackInvalid) return;
            CommonDamage(amount);
            EnemyView.EnemyHurtCrit.Invoke();
        }


        public void EnemyDamageWithFall(float amount,int forceDirX)
        {
            if (EnemyData.AttackInvalid) return;
            CommonDamage(amount);
            Rb.velocity = Vector2.zero;
            Rb.AddForce(new Vector2(forceDirX, 0).normalized*EnemyData.HitForceForward*2,ForceMode2D.Impulse);
            EnemyView.EnemyHurtCrit.Invoke();
        }

        private void CommonDamage(float amount)
        {
            if (EnemyData.Defense >= amount)
            {
                EnemyData.CurrentHealth -= 1;
            }
            else
            {
                EnemyData.CurrentHealth += (EnemyData.Defense - amount);
            }
            if (EnemyData.CurrentHealth <= 0)
            {
                EnemyView.EnemyDeath?.Invoke();
            }
        }
    }
}
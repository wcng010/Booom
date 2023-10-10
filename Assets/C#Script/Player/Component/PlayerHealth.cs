﻿using System;
using C_Script.BaseClass;
using C_Script.Common.Model.EventCentre;
using C_Script.Manager;
using C_Script.Player.Core;
using C_Script.Player.Data;
using C_Script.Player.MVC.Model;
using UnityEngine;

namespace C_Script.Player.Component
{
    public sealed class PlayerHealth: CoreComponent
    {
        private PlayerModel PlayerModel => Model as PlayerModel;
        private PlayerData PlayerData => PlayerModel.PlayerData;
        private Rigidbody2D Rb => PlayerModel.PlayerRigidbody2D;
        private Transform PlayerTrans => PlayerModel.PlayerTrans;
        
        private GameObject _hitEffect;

        private void OnEnable()
        {
            InputManager.Instance.KeyEventE?.AddListener(RecoverHealth);
        }

        private void OnDisable()
        {
            InputManager.Instance.KeyEventE?.RemoveListener(RecoverHealth);
        }

        public void PlayerDamage(float amount,Vector2 forceVector2,ForceDirection forceDir)=>Damage(PlayerData,amount,forceVector2,forceDir);
        
        private void Damage(AttackObjectDataSo attackObjectDataSo, float amount, Vector2 forceVector2,ForceDirection forceDir)
        {
            if (attackObjectDataSo.Defense >= amount)
            {
                attackObjectDataSo.CurrentHealth -= 1;
            }
            else
            {
                attackObjectDataSo.CurrentHealth += (attackObjectDataSo.Defense - amount);
            }
            if (attackObjectDataSo.CurrentHealth <= 0)
            {
                CombatEventCentreManager.Instance.Publish(CombatEventType.PlayerDeath);
            }
            switch (forceDir)
            {
                case ForceDirection.None : 
                    break;
                case ForceDirection.Up :
                    Rb.velocity = Vector2.zero; Rb.AddForce(forceVector2.normalized * attackObjectDataSo.HitForceUp, ForceMode2D.Impulse);
                    CombatEventCentreManager.Instance.Publish(CombatEventType.PlayerHurt);
                    break;
                case ForceDirection.Down :
                    Rb.velocity = Vector2.zero; Rb.AddForce(forceVector2.normalized * attackObjectDataSo.HitForceDown, ForceMode2D.Impulse);
                    CombatEventCentreManager.Instance.Publish(CombatEventType.PlayerHurt);
                    break;
                case ForceDirection.Forward:
                    Rb.velocity = Vector2.zero; Rb.AddForce(forceVector2.normalized * attackObjectDataSo.HitForceForward, ForceMode2D.Impulse);
                    CombatEventCentreManager.Instance.Publish(CombatEventType.PlayerHurt);
                    break;
                default: Debug.unityLogger.LogError("LogicError","No ForceDir Setting");
                    break;
            }
            if(_hitEffect) 
                Destroy(_hitEffect);
            _hitEffect = Instantiate(PlayerData.PlayerWounded, transform.parent, true);
            _hitEffect.transform.localPosition = new Vector3(0,PlayerData.WoudedEffectOffSetY,0);
        }
        
        public void FireDamage(float amount)
        {
            if ((PlayerData.CurrentHealth -= amount) < 0)
            {
                CombatEventCentreManager.Instance.Publish(CombatEventType.PlayerDeath);
            }
            Rb.velocity = Vector2.zero;
            Rb.AddForce(new Vector2(-PlayerTrans.localScale.x,1).normalized*PlayerData.HitForceUp,ForceMode2D.Impulse);
            CombatEventCentreManager.Instance.Publish(CombatEventType.PlayerHurt);
        }
        
        public void StarDamage(float amount)
        {
            if ((PlayerData.CurrentHealth -= amount) < 0)
            {
                CombatEventCentreManager.Instance.Publish(CombatEventType.PlayerDeath);
            }
            Rb.velocity = Vector2.zero;
            Rb.AddForce(new Vector2(-PlayerTrans.localScale.x,1).normalized*PlayerData.HitForceUp,ForceMode2D.Impulse);
            CombatEventCentreManager.Instance.Publish(CombatEventType.PlayerHurt);
        }

        public void FatalBlow()
        {
            PlayerData.CurrentHealth = 0;
            CombatEventCentreManager.Instance.Publish(CombatEventType.PlayerDeath);
        }

        private void RecoverHealth()
        {
            var maxHealth = PlayerData.MaxHealth;
            if (GameManager.Instance.cardRecord.BloodBottleUpTimes > 0)
            {
                if (PlayerData.CurrentHealth + 0.5 * maxHealth > maxHealth)
                {
                    PlayerData.CurrentHealth = maxHealth;
                }
                else
                {
                    PlayerData.CurrentHealth += 0.5f * maxHealth;
                }
                GameManager.Instance.cardRecord.BloodBottleUpTimes--;
                CombatEventCentreManager.Instance.Publish(CombatEventType.UseBloodBottle);
            }
        }
    }
}
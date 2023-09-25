using System;
using System.Collections.Generic;
using C_Script.Common.Model.EventCentre;
using C_Script.Manager;
using C_Script.Player.Data;
using UnityEngine;
namespace C_Script.UI.SkillBar
{
    public class SkillManager : MonoBehaviour
    {
       [SerializeField] public SkillData skillData;

       private void OnEnable()
       {
           CombatEventCentreManager.Instance.Subscribe(CombatEventType.UpdateAllData,UpdateSkillData);
       }

       private void OnDisable()
       {
           CombatEventCentreManager.Instance.Unsubscribe(CombatEventType.UpdateAllData,UpdateSkillData);
       }

       private void UpdateSkillData()
       {
           if (GameManager.Instance.cardRecord.WaterFallSkill)
           {
               transform.Find("WaterWave").gameObject.SetActive(true);
           }
           if (GameManager.Instance.cardRecord.PlayerDashSkill)
           {
               transform.Find("Dash").gameObject.SetActive(true);
           }
           SkillCool[] skills = GetComponentsInChildren<SkillCool>();
           foreach (var skill in skills)
           {
               skill.coolDown *= (1 - GameManager.Instance.cardRecord.PlayerCoolReduceAmount());
           }
       }
    }
}

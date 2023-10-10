using System;
using System.Collections.Generic;
using C_Script.Common.Model.EventCentre;
using C_Script.Common.Model.Singleton;
using C_Script.Manager;
using C_Script.Player.Data;
using UnityEngine;
namespace C_Script.UI.SkillBar
{
    public class SkillManager : NormSingleton<SkillManager>
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
           InitSkills();
           if (GameManager.Instance.cardRecord.WaterWaveSkill)
           {
               transform.Find("WaterWave").gameObject.SetActive(true);
           }
           if (GameManager.Instance.cardRecord.PlayerDashSkill)
           {
               transform.Find("Dash").gameObject.SetActive(true);
           }
           if (GameManager.Instance.cardRecord.WaterBlastSkill)
           {
               transform.Find("WaterBlast").gameObject.SetActive(true);
           }
           if (GameManager.Instance.cardRecord.PlayerBigFallSkill)
           {
               transform.Find("Fall").gameObject.SetActive(true);
           }
           SkillCool[] skills = GetComponentsInChildren<SkillCool>();
           foreach (var skill in skills)
           {
               skill.coolDown *= (1 - GameManager.Instance.cardRecord.PlayerCoolReduceAmount());
           }
       }


       private void InitSkills()
       {
           skillData.commonSkills["Dash"] = false;
           skillData.commonSkills["BigFall"] = false;
           skillData.waterSkills["WaterBlast"] = false;
           skillData.waterSkills["WaterWave"] = false;
       }

    }
}

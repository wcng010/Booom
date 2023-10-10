using System;
using System.Collections.Generic;
using C_Script.BaseClass;
using C_Script.Common.Interface;
using C_Script.Common.Model.ObjectPool;
using C_Script.UI.SkillBar;
using Mono.Cecil;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace C_Script.Player.Component
{
    public class SkillComponent : CoreComponent
    {
        
        [SerializeReference] public Dictionary<ObjectType, GameObject> ObjectDictionary = new Dictionary<ObjectType, GameObject>();
        public ObjectListSo objectListSo;
        
        //实际上单纯是在对象池中激活该Skill物体。
        //
        //public void Skill() =>
           // ObjectDictionary?[skillType].GetComponent<ISkill>().Skill();
        public void Skill()
        {
            if(SkillManager.Instance.skillData.waterSkills["WaterWave"])
                ObjectDictionary?[ObjectType.WaterWave].GetComponent<ISkill>().Skill();
            else if(SkillManager.Instance.skillData.waterSkills["WaterBlast"])
                ObjectDictionary?[ObjectType.WaterBlast].GetComponent<ISkill>().Skill();
        }
        private void Start()
        {
            //遍历objectListSo的objList中的预制体队列。
            foreach (var obj in objectListSo.objList)
            {
                //获取到obj的技能种类，和预制体实体添加到字典中
                ObjectDictionary.Add(obj.GetComponent<ISkill>().SkillType(),obj);
                //压入对象池
                BigObjectPool.Instance.PushObject(obj.GetComponent<ISkill>().SkillType(),Instantiate(obj));
            }
        }
    }
}

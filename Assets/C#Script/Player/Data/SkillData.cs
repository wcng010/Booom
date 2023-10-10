using System;
using System.Collections.Generic;
using C_Script.Common.Model.ObjectPool;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

namespace C_Script.Player.Data
{
    [CreateAssetMenu(fileName = "Data",menuName = "Data/SkillBool")]
    public class SkillData : ScriptableObject
    {
        public SerializedDictionary<string, bool> waterSkills = new SerializedDictionary<string, bool>();
        public SerializedDictionary<string,bool> commonSkills = new SerializedDictionary<string, bool>();
    }
}

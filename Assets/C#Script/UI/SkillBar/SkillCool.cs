using System;
using System.Collections;
using C_Script.Player.Data;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace C_Script.UI.SkillBar
{
    public abstract class SkillCool: MonoBehaviour
    {
        [SerializeField] public float coolDown;
        public abstract void UpdateSkillCool();
    }
}

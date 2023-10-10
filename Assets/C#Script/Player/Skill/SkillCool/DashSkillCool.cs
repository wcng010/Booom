using System.Collections;
using C_Script.Manager;
using C_Script.Player.Data;
using C_Script.UI.SkillBar;
using UnityEngine;
using UnityEngine.UI;

namespace C_Script.Player.Skill.SkillCool
{
    public class DashSkillCool : ComonSkillCool
    {
        protected override void OnEnable()
        {
            InputManager.Instance.KeyEventQ.AddListener(UpdateSkillCool);
        }

        protected override void OnDisable()
        {
            InputManager.Instance.KeyEventQ.RemoveListener(UpdateSkillCool);
        }
    }
}

using C_Script.Manager;

namespace C_Script.Player.Skill.SkillCool
{
    public class WaterBlastSkillCool:WaterSkillCool
    {
        protected override void OnEnable()
        {
            InputManager.Instance.KeyEventAlpha2.AddListener(UpdateSkillCool);
        }

        protected override void OnDisable()
        {
            InputManager.Instance.KeyEventAlpha2.RemoveListener(UpdateSkillCool);
        }
    }
}
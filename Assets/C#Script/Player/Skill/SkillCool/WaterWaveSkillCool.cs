using C_Script.Manager;

namespace C_Script.Player.Skill.SkillCool
{
    public class WaterWaveSkillCool:WaterSkillCool
    {
        protected override void OnEnable()
        {
            InputManager.Instance.KeyEventAlpha1.AddListener(UpdateSkillCool);
        }

        protected override void OnDisable()
        {
            InputManager.Instance.KeyEventAlpha1.RemoveListener(UpdateSkillCool);
        }
    }
}
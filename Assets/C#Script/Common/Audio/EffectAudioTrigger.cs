using C_Script.Manager;
using C_Script.Player.Skill.RemoteSkill;

namespace C_Script.Common.Audio
{
    public class EffectAudioTrigger:AudioTrigger
    {
        public void WaterWaveBreak() => AudioManager.Instance.WaterBreakPlay();
    }
}
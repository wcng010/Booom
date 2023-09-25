using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
namespace C_Script.Save.Gain_Debuff_Record
{
    [CreateAssetMenu(menuName = "Data",fileName = "Data/CardRecord")]
    public class CardRecord : ScriptableObject
    {

        [field: SerializeField] [field: FoldoutGroup("Card")] public List<Sprite> AngleCard = new List<Sprite>();
        [field: SerializeField] [field: FoldoutGroup("Card")] public List<Sprite> DemonCard = new List<Sprite>();
        [field:SerializeField][field: FoldoutGroup("Buff Effect")] public int PlayerHealthUpTimes { get; set; }
        [field:SerializeField][field: FoldoutGroup("Buff Effect")] public float PlayerHealthUpRate { get; set; }
        public float PlayerHealthUpAmount() => PlayerHealthUpTimes * PlayerHealthUpRate;
        [field:SerializeField][field: FoldoutGroup("Buff Effect")] public int PlayerCoolReduceTimes { get; set; }
        [field:SerializeField][field: FoldoutGroup("Buff Effect")] public float PlayerCoolReduceRate { get; set; }
        public float PlayerCoolReduceAmount() => PlayerCoolReduceTimes * PlayerCoolReduceRate;
        [field:SerializeField][field: FoldoutGroup("Buff Effect")] public int PlayerSpeedUpTimes { get; set; }
        [field:SerializeField][field: FoldoutGroup("Buff Effect")] public float PlayerSpeedUpRate { get; set; }
        public float PlayerSpeedUpAmount() => PlayerSpeedUpTimes * PlayerSpeedUpRate;
        [field: SerializeField] [field: FoldoutGroup("Buff Effect")] public int PlayerAttackUpTimes { get; set; }
        [field: SerializeField] [field: FoldoutGroup("Buff Effect")] public float PlayerAttackUpRate { get; set; }
        public float PlayerAttackUpAmount() => PlayerAttackUpTimes * PlayerAttackUpRate;
        [field:SerializeField][field: FoldoutGroup("Buff Effect")] public int BloodBottleUpTimes { get; set; }
        [field:SerializeField][field: FoldoutGroup("Buff Effect")] public bool WaterFallSkill{ get; set; }
        [field:SerializeField][field: FoldoutGroup("Buff Effect")] public bool PlayerDashSkill { get; set; }

        [field:SerializeField][field: FoldoutGroup("Debuff Effect")] public int EnemyNumUpTimes { get; set; }
        [field:SerializeField][field: FoldoutGroup("Debuff Effect")] public int EnemyNumUpNum { get; set; }
        public int EnemyNumUpAmount() => EnemyNumUpTimes * EnemyNumUpNum;
        [field: SerializeField] [field: FoldoutGroup("Debuff Effect")] public int EnemyHealthUpTimes { get; set; }
        [field: SerializeField] [field: FoldoutGroup("Debuff Effect")] public float EnemyHealthUpRate { get; set; }
        public float EnemyHealthUpAmount() => EnemyHealthUpTimes * EnemyHealthUpRate;
        [field:SerializeField][field: FoldoutGroup("Debuff Effect")] public int EnemyAttackUpTimes { get; set; }
        [field: SerializeField] [field: FoldoutGroup("Debuff Effect")] public float EnemyAttackUpRate { get; set; }
        public float EnemyAttackUpAmount() => EnemyAttackUpTimes * EnemyAttackUpRate;
        [field:SerializeField][field: FoldoutGroup("Debuff Effect")] public int EnemyDefenseUpTimes { get; set; }
        [field: SerializeField] [field: FoldoutGroup("Debuff Effect")] public float EnemyDefenseUpRate { get; set; }
        public float EnemyDefenseUpAmount() => EnemyDefenseUpTimes * EnemyDefenseUpRate;
    }
}

using System;
using C_Script.Common.Model.EventCentre;
using C_Script.Common.Model.Singleton;
using C_Script.Save.Gain_Debuff_Record;
using UnityEngine;
using UnityEngine.Serialization;


namespace C_Script.Manager
{
    internal class GameManager : HungrySingleton<GameManager>
    {
        public CardRecord cardRecord;
        public int endLoopCount;
        private void ClearRecord()
        {
            PlayerPrefs.SetInt("LoopCount",0);
            cardRecord.BloodBottleUpTimes = 0;
            cardRecord.PlayerHealthUpTimes = 0;
            cardRecord.PlayerSpeedUpTimes = 0;
            cardRecord.PlayerCoolReduceTimes = 0;
            cardRecord.PlayerAttackUpTimes = 0;
            cardRecord.WaterWaveSkill = false;
            cardRecord.WaterBlastSkill = false;
            cardRecord.PlayerDashSkill = false;
            cardRecord.PlayerBigFallSkill = false;
            cardRecord.EnemyNumUpTimes = 0;
            cardRecord.EnemyHealthUpTimes = 0;
            cardRecord.EnemyDefenseUpTimes = 0;
            cardRecord.EnemyAttackUpTimes = 0;
            
            cardRecord.AngleCard.Clear();
            cardRecord.DemonCard.Clear();
        }
        private void OnEnable()
        {
            ScenesEventCentreManager.Instance.Subscribe(ScenesEventType.ClearRecord,ClearRecord);
        }

        private void OnDisable()
        {
            ScenesEventCentreManager.Instance.Unsubscribe(ScenesEventType.ClearRecord,ClearRecord);
        }
        
        void OnApplicationQuit()
        {
            ClearRecord();
        }
    }
}

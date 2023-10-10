using C_Script.Common.Model.EventCentre;
using C_Script.Manager;
using UnityEngine;
using UnityEngine.UI;
namespace C_Script.UI.Card
{
    public class AngleCard : Card
    {
        public override void OnButtonClick()
        {
            Value = Random.Range(0, cardSprite.Count);
            MyAnimator.SetTrigger(Flip);
            switch (Value)
            {
                case 0:Function1();
                    break;
                case 1:Function2();
                    break; 
                case 2:Function3();
                    break;
                case 3:Function4();
                    break;
                case 4:Function5();
                    break;
                case 5:Function6();
                    break;
                case 6:Function7();
                    break;
                case 7: Function8();
                    break;
                case 8 : Function9();
                    break;
            }
            MyButton.enabled = false;
            CardManager.ClickCount += 1;
            if (CardManager.ClickCount >= 3)
            {
                Invoke(nameof(CloseCardPanel),2f);
            }
        }

        public void FilpTheCard()
        {
            MyImage.sprite = cardSprite[Value];
            GameManager.Instance.cardRecord.AngleCard.Add(MyImage.sprite);
        }
        protected void CloseCardPanel()
        {
            CombatEventCentreManager.Instance.Publish(CombatEventType.UpdateAllData);
            CollectSprite();
            CardManager.gameObject.SetActive(false);
        }
        
        protected override void Function1() => PlayerHealthUp();
        protected override void Function2() => WaterWaveOpen();
        protected override void Function3() => PlayerCoolReduce();
        protected override void Function4() => PlayerDashOpen();
        protected override void Function5() => PlayerSpeedUp();
        protected override void Function6() => BloodBottleUp();
        protected override void Function7() => PlayerAttackUp();
        protected void Function8() => WaterBlastOpen();
        protected void Function9() => BigFallOpen();
        
        private void PlayerHealthUp() => GameManager.Instance.cardRecord.PlayerHealthUpTimes++;
        private void WaterWaveOpen() => GameManager.Instance.cardRecord.WaterWaveSkill = true;
        private void WaterBlastOpen() => GameManager.Instance.cardRecord.WaterBlastSkill = true;
        private void BigFallOpen() => GameManager.Instance.cardRecord.PlayerBigFallSkill = true;
        private void PlayerCoolReduce() => GameManager.Instance.cardRecord.PlayerCoolReduceTimes++;
        private void PlayerDashOpen() => GameManager.Instance.cardRecord.PlayerDashSkill = true;
        private void PlayerSpeedUp() => GameManager.Instance.cardRecord.PlayerSpeedUpTimes++;
        private void BloodBottleUp() => GameManager.Instance.cardRecord.BloodBottleUpTimes++;
        private void PlayerAttackUp() => GameManager.Instance.cardRecord.PlayerAttackUpTimes++;
    }
}

using C_Script.Common.Model.EventCentre;
using C_Script.Manager;
using Random = UnityEngine.Random;

namespace C_Script.UI.Card
{
    public class DemonCard : Card
    {
        public override void OnButtonClick()
        {
            Value = Random.Range(0, cardSprite.Count-1);
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
            }
            MyButton.enabled = false;
            CardManager.ClickCount += 1;
            if (CardManager.ClickCount >= 3)
            {
                Invoke(nameof(CloseCardPanel),3f);
            }
        }

        public void FilpTheCard()
        {
            MyImage.sprite = cardSprite[Value];
            GameManager.Instance.cardRecord.DemonCard.Add(MyImage.sprite);
        }
        protected void CloseCardPanel()
        {
            CombatEventCentreManager.Instance.Publish(CombatEventType.UpdateAllData);
            CollectSprite();
            CardManager.gameObject.SetActive(false);
        }
        
        protected override void Function1() => EnemyAttackUp();
        protected override void Function2() => EnemyDefenseUp();
        protected override void Function3() => EnemyNumUp();
        protected override void Function4() => EnemyHealthUp();
        protected override void Function5(){}
        protected override void Function6(){}
        protected override void Function7(){}
        private void EnemyNumUp() => GameManager.Instance.cardRecord.EnemyNumUpTimes++;
        private void EnemyHealthUp() => GameManager.Instance.cardRecord.EnemyHealthUpTimes++;
        private void EnemyAttackUp() => GameManager.Instance.cardRecord.EnemyAttackUpTimes++;
        private void EnemyDefenseUp() => GameManager.Instance.cardRecord.EnemyDefenseUpTimes++;
    }
}
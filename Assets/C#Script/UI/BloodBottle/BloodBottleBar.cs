using System;
using C_Script.Common.Model.EventCentre;
using C_Script.Manager;
using UnityEngine;
namespace C_Script.UI.BloodBottle
{
    public class BloodBottleBar : MonoBehaviour
    {
        [SerializeField]private GameObject bloodBottle;

        private void OnEnable()
        {
            CombatEventCentreManager.Instance.Subscribe(CombatEventType.UpdateAllData,UpdateBloodBottle);
            InputManager.Instance.KeyEventE?.AddListener(UpdateBloodBottle);
        }

        private void OnDisable()
        {
            CombatEventCentreManager.Instance.Subscribe(CombatEventType.UpdateAllData,UpdateBloodBottle);
            InputManager.Instance.KeyEventE?.RemoveListener(UpdateBloodBottle);
        }

        private void UpdateBloodBottle()
        {
            int bottleNum = GameManager.Instance.cardRecord.BloodBottleUpTimes;
            for (int i = 0; i < transform.childCount; ++i)
            {
                if (i < bottleNum) transform.GetChild(i).gameObject.SetActive(true);
                else transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}

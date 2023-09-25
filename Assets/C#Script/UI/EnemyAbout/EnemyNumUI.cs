using System;
using C_Script.Common.Model.EventCentre;
using C_Script.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace C_Script.UI.EnemyAbout
{
    public class EnemyNumUI : MonoBehaviour
    {
        private Text _text;

        private void Awake()
        {
            _text = GetComponent<Text>();
        }

        private void OnEnable()
        {
            CombatEventCentreManager.Instance.Subscribe(CombatEventType.EnemyNumChange,InvokeDelay);
        }

        private void OnDisable()
        {
            CombatEventCentreManager.Instance.Unsubscribe(CombatEventType.EnemyNumChange,InvokeDelay);
        }

        private void InvokeDelay() => Invoke(nameof(UpdateEnemyNum), 0.5f);
        private void UpdateEnemyNum()
        {
            _text.text = "Monster:" + GameObject.FindGameObjectsWithTag("Enemy").Length;
        }
    }
}

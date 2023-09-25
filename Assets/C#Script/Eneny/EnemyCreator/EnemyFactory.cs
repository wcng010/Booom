using System;
using System.Collections.Generic;
using C_Script.Common.Model.EventCentre;
using C_Script.Common.Model.Singleton;
using C_Script.Manager;
using UnityEngine;
using Random = UnityEngine.Random;


namespace C_Script.Eneny.EnemyCreator
{
    public class EnemyFactory : HungrySingleton<EnemyFactory>
    {
        [SerializeField]private int enemyNum;
        [SerializeField]private List<Transform> generatePoints = new List<Transform>();
        [SerializeField]private Transform bossTrans1;
        [SerializeField] private Transform bossTrans2;
        private List<Transform> _activedPoint = new List<Transform>();
        

        private void OnEnable()
        {
            CombatEventCentreManager.Instance.Subscribe(CombatEventType.UpdateAllData,UpdateMonsterData);
        }

        private void OnDisable()
        {
            CombatEventCentreManager.Instance.Unsubscribe(CombatEventType.UpdateAllData,UpdateMonsterData);
        }
        
        private void UpdateMonsterData()
        {
            enemyNum += GameManager.Instance.cardRecord.EnemyNumUpAmount();
            GenerateMonster();
        }

        private void GenerateMonster()
        {
            int loop = PlayerPrefs.GetInt("LoopCount");
            if (loop != 4 && loop != 9)
            {
                if (enemyNum > generatePoints.Count) enemyNum = generatePoints.Count;
                while (_activedPoint.Count < enemyNum)
                {
                    for (int i = 0; i < generatePoints.Count&&_activedPoint.Count<enemyNum; ++i)
                    {
                        if (Random.value > 0.5f)
                        {
                            generatePoints[i].gameObject.SetActive(true);
                            _activedPoint.Add(generatePoints[i]);
                        }
                    }
                }
                CombatEventCentreManager.Instance.Publish(CombatEventType.EnemyNumChange);
            }
            if (loop == 4)
            {
                bossTrans1.gameObject.SetActive(true);
            }

            if (loop == 9)
            {
                bossTrans2.gameObject.SetActive(true);
            }
        }
    }
}

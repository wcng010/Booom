using System;
using System.Collections;
using C_Script.Common.Model.ObjectPool;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace C_Script.Object
{
    public class WarningFire : MonoBehaviour
    {
        [SerializeField] private int waitTime;
        [SerializeField] private Vector2 originPos;
        private float _temp;
        private void Awake()
        {
            BigObjectPool.Instance.PushObject(ObjectType.PurpleStar,gameObject);
        }

        private void OnEnable()
        {
            _temp = 2+Random.value*waitTime;
            StartCoroutine(nameof(WaitForFire));
            transform.position = originPos;
            transform.rotation = Quaternion.identity;
            transform.localScale = new Vector3(1, 1, 1);
        }

        //等待发射时间
        IEnumerator WaitForFire()
        {
            yield return new WaitForSeconds(_temp);
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}

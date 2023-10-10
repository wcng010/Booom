using System;
using System.Collections;
using System.Collections.Generic;
using C_Script.Eneny.EnemyCommon.Component;
using UnityEngine;
using UnityEngine.Serialization;

public class BigFallAsh : MonoBehaviour
{
    [SerializeField]private int power;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            col.GetComponentInChildren<EnemyHealth>().EnemyDamageWithFall(power,(col.transform.position.x - transform.position.x)>0?1:-1);
        }
    }
}

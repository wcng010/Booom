using System;
using System.Collections;
using System.Collections.Generic;
using C_Script.Common.Model.ObjectPool;
using C_Script.Player.Component;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PurpleStar : MonoBehaviour
{
    [SerializeField]private float moveSpeed;
    [SerializeField] private float amount;

    private Rigidbody2D Rb => _rb? _rb : _rb = GetComponent<Rigidbody2D>();
    private Rigidbody2D _rb;
    
    private void OnEnable()
    {
        StartCoroutine(nameof(Fire));
        transform.localPosition = Vector3.zero;
    }
    
    private IEnumerator Fire()
    {
        Rb.velocity = new Vector2(-moveSpeed,0);
        yield return new WaitForSeconds(2f);
        //失活
        Invoke(nameof(SetFalseParent),0.1f);
        gameObject.SetActive(false);
    }
    
    private void SetFalseParent()=>transform.parent.gameObject.SetActive(false);

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.transform.GetComponentInChildren<PlayerHealth>().StarDamage(amount);
        }
    }
}

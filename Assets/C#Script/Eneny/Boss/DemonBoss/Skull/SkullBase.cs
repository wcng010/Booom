using System.Collections;
using C_Script.BaseClass;
using C_Script.Common.Model.ObjectPool;
using C_Script.Player.Component;
using UnityEngine;
using UnityEngine.Serialization;

namespace C_Script.Eneny.Boss.DemonBoss.Skull
{
    public class SkullBase : MonoBehaviour
    {
        [SerializeField] private float fireTime;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float amount;
        private Transform _playerTrans;
        private Vector2 _originPos;
        private int _trigger;
        private Rigidbody2D Rb => _rb? _rb : _rb = GetComponent<Rigidbody2D>();
        private Rigidbody2D _rb;
        private Animator Antor => _animator ? _animator : _animator = GetComponent<Animator>();
        private Animator _animator;
        
        private static readonly int Move = Animator.StringToHash("Move");

        private void Awake()
        {
            _originPos = transform.position;
            _playerTrans = GameObject.FindWithTag("Player").transform;
            BigObjectPool.Instance.PushObject(ObjectType.Skull,gameObject);
        }

        private void OnEnable()
        {
            _trigger = 0;
            transform.position = _originPos;
            transform.rotation = Quaternion.identity;
            transform.localScale = new Vector3(0.5f, 0.5f, 1);
            StartCoroutine(nameof(Fire));
        }
        
        private IEnumerator Fire()
        {
            yield return new WaitForSeconds(fireTime);
            _trigger = 1;
            Antor.SetBool(Move,true);
            if(!_playerTrans) gameObject.SetActive(false);
            Vector2 moveDir = (_playerTrans.position -transform.position).normalized;
            if (moveDir.x <= 0)
            {
                var angle = Mathf.Atan2(-moveDir.y, -moveDir.x) * Mathf.Rad2Deg;
                var trailRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = trailRotation;
            }
            else
            {
                var angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
                transform.localScale = new Vector3(-0.5f,0.5f,1);
                var trailRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = trailRotation;
            }
            Rb.velocity = moveDir * moveSpeed;
            yield return new WaitForSeconds(2f);
            SetActiveFalse();
        }

        private void SetActiveFalse() => gameObject.SetActive(false);
        

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player")&&_trigger == 1)
            {
                col.transform.GetComponentInChildren<PlayerHealth>().PlayerDamage(amount,new Vector2(transform.eulerAngles.z<180? -1:1,0),ForceDirection.Forward);
                SetActiveFalse();
            }
            else if (col.CompareTag("Ground"))
            {
                SetActiveFalse();
            }
        }
    }
}

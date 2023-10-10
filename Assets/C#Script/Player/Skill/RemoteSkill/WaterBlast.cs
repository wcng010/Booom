using System.Collections;
using System.Collections.Generic;
using C_Script.Common.Interface;
using C_Script.Common.Model.ObjectPool;
using C_Script.Eneny.EnemyCommon.Component;
using Sirenix.OdinInspector;
using UnityEngine;

public class WaterBlast : MonoBehaviour, ISkill
{
    [FoldoutGroup("Setting")] public float liveTime;
    [FoldoutGroup("Setting")] public float fireForce;
    [FoldoutGroup("Setting")] public float amount;
    private List<string> enemyNames = new List<string>();
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private Transform _ownerTrans;
    private Transform _myTrans;
    private bool _isBreak;
    private static readonly int IsFire = Animator.StringToHash("IsFire");
    private static readonly int IsBreak = Animator.StringToHash("IsBreak");
    public ObjectType SkillType() => ObjectType.WaterBlast;

    public void Skill()
    {
        BigObjectPool.Instance.SetOneActive(SkillType());
    }

    private void Awake()
    {
        _myTrans = transform;
        _ownerTrans = GameObject.FindWithTag("Player").transform;
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        enemyNames.Clear();
        //发射动画
        _animator.SetBool(IsFire, true);
        //发射加力
        StartCoroutine(nameof(CheckFire));
        var position = _ownerTrans.position;
        var localScale = _ownerTrans.localScale;
        _myTrans.position = new Vector3(position.x + localScale.x * 0.2f, position.y+0.12f, position.z);
        //此处改变方向
        _myTrans.eulerAngles = new Vector3(0, localScale.x > 0 ? 0 : 180, 0);
        //设置BreakDown时间
        Invoke(nameof(BreakDown), liveTime);
        _isBreak = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer==LayerMask.NameToLayer("Ground"))
        {
            BreakDown();
        }

        if (col.CompareTag("Enemy") && !_isBreak)
        {
            //TODO:实现带着Player走的效果
            StartCoroutine(DragEnemyMove(col.transform));
        }
    }
    
    
    //发射加力
    IEnumerator DragEnemyMove(Transform colTrans)
    {
        float t = 0;
        //如果没有水波没有Break
        while (!_isBreak&&t < liveTime)
        {
            t += Time.deltaTime;
            colTrans.position = transform.position;
            yield return null;
        }
        //水波Break后，调用敌人受伤函数。
        if (!enemyNames.Contains(colTrans.gameObject.name))
        {
            colTrans.GetComponentInChildren<EnemyHealth>()
                ?.EnemyDamageWithSkill(amount, new Vector2(transform.rotation.y > 0 ? -1 : 1, 0));
            enemyNames.Add(colTrans.gameObject.name);
        }
    }
    
    IEnumerator CheckFire()
    {
        yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).IsName("WaterMove"));
        if (!_isBreak) _rigidbody2D.AddForce(new Vector2(fireForce * (_ownerTrans.localScale.x > 0 ? 1 : -1), 0), ForceMode2D.Impulse);
        yield return null;
    }
    
    //开启BreakDown
    private void BreakDown()
    {
        if (_isBreak) return;
        _isBreak = true;
        _rigidbody2D.velocity = new Vector2(0, 0);
        _animator.SetTrigger(IsBreak);
        if(gameObject.activeSelf) StartCoroutine(nameof(CheckBreak));
    }
    
    //BreakDown动画结束，失活该Effect
    IEnumerator CheckBreak()
    {
        yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).IsName("WaterBreak"));
        yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.95f);
        gameObject.SetActive(false);
    }
}

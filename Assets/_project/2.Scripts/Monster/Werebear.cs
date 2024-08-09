using System.Collections;
using UnityEngine;

public class Werebear : Monster, IDamageable
{
    [SerializeField] private Collider2D _target;

    private Animator _ani;
    private Rigidbody2D _rigid;

    private float _attackTimer = 0.0f;
    private float _attackDelay = 1.0f;
    private float _attackRange = 2.0f;
    private float _attackFinishTime = 0.9f;

    private bool _isDie = false;
    private bool _isKnockBack = false;
    private bool _isAttack = false;
    [SerializeField] private MonsterState _curState = MonsterState.Idle;
    public override void Init(int id)
    {
        base.Init(id);

        _ani = GetComponent<Animator>();
        _rigid = GetComponent<Rigidbody2D>();
        ChangeState(MonsterState.Idle);
    }
    private void Update()
    {
        _target = Physics2D.OverlapCircle(transform.position, _attackRange, LayerMask.GetMask("Player"));
        _attackTimer += Time.deltaTime;

        ChangeState(_curState);
    }
    private void ChangeState(MonsterState state)
    {
        _curState = state;
        switch (state)
        {
            case MonsterState.Idle:
                Idle();
                break;
            case MonsterState.Move:
                Move();
                break;
            case MonsterState.Attack:
                Attack();
                break;
            case MonsterState.Hit:
                Hit();
                break;
            case MonsterState.Die:
                OnDie();
                break;
        }
    }
    private void Idle()
    {
        _ani.SetInteger("State", (int)MonsterState.Idle);
        _rigid.velocity = new Vector2(0, _rigid.velocity.y);
        if (_target != null)
        {
            if (_attackTimer >= _attackDelay)
                ChangeState(MonsterState.Attack);
        }
        else
        {
            ChangeState(MonsterState.Move);
        }
    }
    private void Move()
    {
        _rigid.velocity = new Vector2(-_monsterData.speed, _rigid.velocity.y);
        _ani.SetInteger("State", (int)MonsterState.Move);
        if (_target != null)
        {
            ChangeState(MonsterState.Attack);
        }
    }
    private void Attack()
    {
        if (_isAttack) return;

        _attackTimer = 0;
        _isAttack = true;
        _rigid.velocity = new Vector2(0, _rigid.velocity.y);
        _ani.SetInteger("State", (int)MonsterState.Attack);
        StartCoroutine(C_Attack());
    }
    IEnumerator C_Attack()
    {
        yield return new WaitForSeconds(_attackFinishTime);
        ChangeState(MonsterState.Idle);
        _isAttack = false;
    }

    public void OnDamage(float _damage)
    {
        if (_isDie || _isKnockBack) return;

        _monsterData.health -= _damage;
        if (_monsterData.health <= 0)
        {
            ChangeState(MonsterState.Die);
            return;
        }

        if (_target != null) return;
        ChangeState(MonsterState.Hit);
    }
    public void Hit()
    {
        if (_isKnockBack) return;

        StartCoroutine(C_KnockBack());
        _ani.SetTrigger("Hit");
    }
    private IEnumerator C_KnockBack()
    {
        _isKnockBack = true;
        _rigid.AddForce(Vector2.right * 2.0f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(1.0f);
        ChangeState(MonsterState.Idle);
        _isKnockBack = false;
    }
    public void OnDie()
    {
        if (_isDie) return;

        _isDie = true;
        _rigid.velocity = Vector2.zero;
        _ani.SetTrigger("Die");
        this.gameObject.layer = 1;
        Destroy(gameObject, 2.0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }
}
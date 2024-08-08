using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Weapon")]
    [SerializeField] private Arrow _arrow;

    [Header("PlayerInfo")]
    [SerializeField] private float _attackRange = 5.0f;
    [SerializeField] private float _attackSpeed = 1.0f;

    private float _attackTimer = 0.0f;

    private Animator _ani;

    public void Start()
    {
        Init();
    }
    public void Init()
    {
        _ani = GetComponent<Animator>();
    }

    private void Update()
    {
        Attack();
    }
    public void Attack()
    {
        _attackTimer += Time.deltaTime;
        if (_attackTimer >= _attackSpeed)
        {
            // 현재 몬스터가 한마리 뿐이여서 상관 없다
            Collider2D _object = Physics2D.OverlapCircle(transform.position, _attackRange, LayerMask.GetMask("Monster"));
            if (_object != null)
            {
                Arrow _arrowObject = Instantiate(_arrow, transform.position, Quaternion.identity);
                // 플레이어는 고정 오른쪽에서만 몬스터가 나온다
                _arrowObject.Shoot(Vector2.right);
                _attackTimer = 0.0f;
                _ani.SetTrigger("Skill1");
            }
        }
        return;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }
}
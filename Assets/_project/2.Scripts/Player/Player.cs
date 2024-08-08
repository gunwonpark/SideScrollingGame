using UnityEngine;

/// <summary>
/// 플레이어는 좌우로만 이동이 가능하다.
/// </summary>
[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _body;
    private Animator _ani;
    private PlayerData _playerData => DataManager.Instance.PlayerData;

    private bool _isMove = false;

    private void Start()
    {
        Init();
    }
    public void Init()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _body = GetComponent<SpriteRenderer>();
        _ani = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        Move();
    }
    public void Move()
    {
        float _inputX = Input.GetAxisRaw("Horizontal");

        if (_inputX != 0)
        {
            Vector2 _movePosition = (Vector2)transform.position + Vector2.right * _inputX * _playerData.moveSpeed * Time.fixedDeltaTime;
            _rigidbody2D.MovePosition(_movePosition);

            _body.flipX = (_inputX == 1) ? false : true;

            SetState(PlayerState.Move);
        }
        else
        {
            SetState(PlayerState.Idle);
            _isMove = false;
        }
    }

    public void SetState(PlayerState _state)
    {
        switch (_state)
        {
            case PlayerState.Idle:
                _ani.SetInteger("State", (int)PlayerState.Idle);
                break;
            case PlayerState.Move:
                _ani.SetInteger("State", (int)PlayerState.Move);
                break;
            case PlayerState.Attack:
                _ani.SetInteger("State", (int)PlayerState.Attack);
                break;
            case PlayerState.Die:
                _ani.SetInteger("State", (int)PlayerState.Die);
                break;
        }
    }
}
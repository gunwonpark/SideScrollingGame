using System;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] // È®ÀÎ¿ë
    protected MonsterData _monsterData;

    public event Action OnDieEvent;

    public virtual void Init(int id)
    {
        _monsterData = new MonsterData(DataManager.Instance.MonsterDatas.GetData(id));
    }

    private void OnDestroy()
    {
        OnDieEvent?.Invoke();
    }
}
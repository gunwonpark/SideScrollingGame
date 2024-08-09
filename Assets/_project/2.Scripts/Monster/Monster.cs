using System;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] // Ȯ�ο�
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
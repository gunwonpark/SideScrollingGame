using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private int[] _monsterIds;
    [SerializeField] private List<Monster> _monsters;
    [SerializeField] private Transform _spawnPoint;
    private int _maxMonsterIndex = 0;
    private int _monsterIndex = 0;
    private void Start()
    {
        Init();
        CreateMonster();
    }
    private void CreateMonster()
    {
        Monster _monster = Instantiate(_monsters[_monsterIndex], _spawnPoint.position, Quaternion.identity);
        _monster.Init(_monsterIds[_monsterIndex]);
        _monster.OnDieEvent += OnDieMonster;
        _monsterIndex++;
        if (_monsterIndex >= _maxMonsterIndex)
        {
            _monsterIndex = 0;
        }
    }
    private void OnDieMonster()
    {
        CreateMonster();
    }

    public void Init()
    {
        _maxMonsterIndex = _monsterIds.Length;

        for (int i = 0; i < _maxMonsterIndex; i++)
        {
            MonsterData _monsterData = DataManager.Instance.MonsterDatas.GetData(_monsterIds[i]);
            GameObject _monsterPrefab = Resources.Load<GameObject>($"Prefabs/Monsters/{_monsterData.name}");
            _monsters.Add(_monsterPrefab.GetComponent<Monster>());
        }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct MonsterData
{
    public int id;
    public string name;
    public MonsterGrade grade;
    public float speed;
    public float health;

    public MonsterData(MonsterData _monsterData)
    {
        this.id = _monsterData.id;
        this.name = _monsterData.name;
        this.grade = _monsterData.grade;
        this.speed = _monsterData.speed;
        this.health = _monsterData.health;
    }

}

[Serializable]
public class MonsterDataBase : DataBase<MonsterData>
{
    public List<MonsterData> _monsterDatas = new List<MonsterData>();

    public override void LoadData(string[] _datas)
    {
        if (_datas == null)
        {
            Debug.Log("There is no data in the file.");
            return;
        }

        foreach (string _data in _datas)
        {
            string[] _splitData = _data.Split(',');

            MonsterData _monsterData = new MonsterData();
            _monsterData.id = int.Parse(_splitData[0]);
            _monsterData.name = _splitData[1];
            _monsterData.grade = (MonsterGrade)Enum.Parse(typeof(MonsterGrade), _splitData[2]);
            _monsterData.speed = float.Parse(_splitData[3]);
            _monsterData.health = float.Parse(_splitData[4]);

            _monsterDatas.Add(_monsterData);
        }

        foreach (MonsterData _data in _monsterDatas)
        {
            this._datas.Add((int)_data.id, _data);
        }
    }
}

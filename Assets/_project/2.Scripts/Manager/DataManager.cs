using System.IO;
using System.Linq;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    [field: SerializeField] public MonsterDataBase MonsterDatas { get; private set; }

    public void Init()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        string[] _monsterDataString = ReadCsv("MonsterData");
        MonsterDatas.Init(_monsterDataString);
    }

    public string[] ReadCsv(string _fileName)
    {
        string _fullFilePath = Path.Combine(Application.dataPath, "Datas", $"{_fileName}.csv");

        if (!File.Exists(_fullFilePath))
        {
            Debug.LogError("There is no file in the path: " + _fullFilePath);
            return null;
        }

        string[] _datas = File.ReadAllLines(_fullFilePath);

        if (_datas.Length < 2)
        {
            Debug.LogError("There is no data in the file.");
            return null;
        }

        return _datas.Skip(1).ToArray();
    }
}
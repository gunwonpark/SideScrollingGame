using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    // �ӽ÷� scriptable object�� ����Ͽ����ϴ�.

    [field: SerializeField]
    public PlayerData PlayerData { get; private set; }

    public void Init()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
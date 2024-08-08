using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    // 임시로 scriptable object를 사용하였습니다.

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
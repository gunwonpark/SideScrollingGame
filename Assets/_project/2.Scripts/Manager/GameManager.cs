using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private DataManager _dataManager;
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        _dataManager.Init();
    }
}

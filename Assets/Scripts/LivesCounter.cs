using UnityEngine;
using UnityEngine.Events;

public class LivesCounter : MonoBehaviour
{
    [SerializeField] private int _maxnumOfLives = 3;
    [SerializeField] private GameObject livesPrefab;
    [SerializeField] private Transform livesParent;
    private static LivesCounter _instance;
    private static int _persistedLives = -1;

    private int _numOfLives;
    public UnityEvent OutOfLives;

    public int NumOfLives
    {
        get => _numOfLives;
        private set
        {
            if (value < 0) OutOfLives?.Invoke();
            _numOfLives = Mathf.Clamp(value, 0, _maxnumOfLives);
            _persistedLives = _numOfLives;
            RefreshUI();
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        if (_persistedLives < 0) _persistedLives = _maxnumOfLives;
        _numOfLives = Mathf.Clamp(_persistedLives, 0, _maxnumOfLives);
    }

    private void Start()
    {
        RefreshUI();
    }

    public void Addlife(int num = 1)
    {
        NumOfLives += num;
    }

    public void Removelife(int num = 1)
    {
        NumOfLives -= num;
        if (NumOfLives == 0)
        {
            ZeroLife();
        }
    }

    private void RefreshUI()
    {
        for (int i = livesParent.childCount - 1; i >= 0; i--)
            Destroy(livesParent.GetChild(i).gameObject);

        for (int i = 0; i < _numOfLives; i++)
            Instantiate(livesPrefab, livesParent);
    }
    public void ZeroLife()
    {
        
    }
}
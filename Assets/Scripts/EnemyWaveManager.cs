using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    public static EnemyWaveManager Instance { get; private set; }

    public event System.EventHandler OnWaveNumberChanged;
    public int getWaveNumber => _waveNumber;
    public float getNextWaveSpawnTimer => _nextWaveSpawnTimer;
    public Vector3 getSpawnPosition => _spawnPosition;

    private enum State
    {
        WaitingToSpawnNextWave,
        SpawningWave
    }

    [SerializeField] private List<Transform> _spawnPositionTransform;
    [SerializeField] private Transform _nextWaveSpawnPositionTransform;

    private float _nextWaveSpawnTimer;
    private float _nextEnemySpawnTimer;
    private int _remainingEnemySpawnAmount;
    private int _waveNumber;
    private State _state;
    private Vector3 _spawnPosition;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _state = State.WaitingToSpawnNextWave;
        _spawnPosition = _spawnPositionTransform[Random.Range(0, _spawnPositionTransform.Count)].position;
        _nextWaveSpawnPositionTransform.position = _spawnPosition;
        _nextWaveSpawnTimer = 3f;
    }

    private void Update()
    {
        switch (_state)
        {
            case State.WaitingToSpawnNextWave:
                _nextWaveSpawnTimer -= Time.deltaTime;
                if (_nextWaveSpawnTimer <= 0)
                {
                    SpawnWave();
                }
                break;
            case State.SpawningWave:
                if (_remainingEnemySpawnAmount > 0)
                {
                    _nextEnemySpawnTimer -= Time.deltaTime;
                    if (_nextEnemySpawnTimer <= 0)
                    {
                        _nextEnemySpawnTimer = Random.Range(0f, 0.2f);
                        Enemy enemy = UtilsClass.Instantiator<Enemy>(GameAssets.Instance.pfEnemy, _spawnPosition + UtilsClass.GetRandomDirection() * Random.Range(0f, 10f));
                        _remainingEnemySpawnAmount--;

                        if(_remainingEnemySpawnAmount <= 0)
                        {
                            _state = State.WaitingToSpawnNextWave;
                            _spawnPosition = _spawnPositionTransform[Random.Range(0, _spawnPositionTransform.Count)].position;
                            _nextWaveSpawnPositionTransform.position = _spawnPosition;
                            _nextWaveSpawnTimer = 10f;
                        }
                    }
                }
                break;
            default:
                break;
        }
    }

    private void SpawnWave()
    {        
        _remainingEnemySpawnAmount = 30 + 3 * _waveNumber;
        _state = State.SpawningWave;
        _waveNumber++;
        OnWaveNumberChanged?.Invoke(this, System.EventArgs.Empty);
    }
}

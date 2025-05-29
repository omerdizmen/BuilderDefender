using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyWaveUI : MonoBehaviour
{
    [SerializeField] private EnemyWaveManager _enemyWaveManager;
    private Camera _mainCamera;
    private RectTransform _enemyWaveSpawnIndicator;
    private RectTransform _enemyClosestPositionIndicator;
    private TextMeshProUGUI _waveNumberText;
    private TextMeshProUGUI _waveMessageText;

    private void Awake()
    {
        _waveNumberText = transform.Find("waveNumberText").GetComponent<TextMeshProUGUI>();
        _waveMessageText = transform.Find("waveMessageText").GetComponent<TextMeshProUGUI>();
        _enemyWaveSpawnIndicator = transform.Find("enemyWaveSpawnPositionIndicator").GetComponent<RectTransform>();
        _enemyClosestPositionIndicator = transform.Find("enemyClosestPositionIndicator").GetComponent<RectTransform>();        
    }

    private void Start()
    {
        _mainCamera = Camera.main;
        _enemyWaveManager.OnWaveNumberChanged += EnemyWaveManager_OnWaveNumberChanged;
        SetWaveNumberText("Wave " + _enemyWaveManager.getWaveNumber);

    }

    private void EnemyWaveManager_OnWaveNumberChanged(object sender, System.EventArgs e)
    {
        SetWaveNumberText("Wave " + _enemyWaveManager.getWaveNumber);
    }

    private void Update()
    {
        float nextWaveSpawnTimer = _enemyWaveManager.getNextWaveSpawnTimer;
        if(nextWaveSpawnTimer <= 0)
        {
            SetMessageText("");
        }
        else
        {
            SetMessageText("Next wave in " + nextWaveSpawnTimer.ToString("F1") + " s");
        }

        HandleEnemyWaveSpawnPositionIndicator();
        HandleEnemyClosestPositionIndicator();
    }

    private void HandleEnemyWaveSpawnPositionIndicator()
    {
        Vector3 dirToNextSpawnPostion = (_enemyWaveManager.getSpawnPosition - _mainCamera.transform.position).normalized;
        _enemyWaveSpawnIndicator.anchoredPosition = 300 * dirToNextSpawnPostion;
        _enemyWaveSpawnIndicator.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(dirToNextSpawnPostion));

        float distanceToNextSpawnPosition = Vector3.Distance(_enemyWaveManager.getSpawnPosition, _mainCamera.transform.position);
        _enemyWaveSpawnIndicator.gameObject.SetActive(distanceToNextSpawnPosition > _mainCamera.orthographicSize * 1.5f);
    }

    private void HandleEnemyClosestPositionIndicator()
    {
        float targetRadius = 9999f;
        Collider2D[] collisions = Physics2D.OverlapCircleAll(_mainCamera.transform.position, targetRadius);

        Enemy targetEnemy = null;
        foreach (Collider2D collision in collisions)
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (targetEnemy == null)
                {
                    targetEnemy = enemy;
                }
                else
                {
                    if (Vector3.Distance(transform.position, enemy.transform.position) <
                      Vector3.Distance(transform.position, targetEnemy.transform.position))
                    {
                        targetEnemy = enemy;
                    }
                }
            }
        }

        if (targetEnemy != null)
        {
            Vector3 dirToClosestEnemy = (targetEnemy.transform.position - _mainCamera.transform.position).normalized;
            _enemyClosestPositionIndicator.anchoredPosition = 250 * dirToClosestEnemy;
            _enemyClosestPositionIndicator.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(dirToClosestEnemy));

            float distanceToClosestEnemy = Vector3.Distance(_enemyWaveManager.getSpawnPosition, _mainCamera.transform.position);
            _enemyClosestPositionIndicator.gameObject.SetActive(distanceToClosestEnemy > _mainCamera.orthographicSize * 1.5f);
        }
        else
        {
            _enemyClosestPositionIndicator.gameObject.SetActive(false);
        }
    }

    private void SetMessageText(string text)
    {
        _waveMessageText.SetText(text);
    }

    private void SetWaveNumberText(string text)
    {
        _waveNumberText.SetText(text);
    }
}

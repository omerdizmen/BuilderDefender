using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float _shootTimerMax;

    private Enemy _targetEnemy;
    private float _shootTimer;
    private float _lookForTargetTimer;
    private float _lookForTargetTimerMax = 0.2f;
    private Transform _projectileSpawnPosition;

    private void Awake()
    {
        _projectileSpawnPosition = transform.Find("projectileSpawnPosition");
    }

    private void Update()
    {
        HandleTargeting();
        HandleShooting();
    }

    private void LookForTargets()
    {
        float targetRadius = 23f;
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, targetRadius);

        foreach (Collider2D collision in collisions)
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (_targetEnemy == null)
                {
                    _targetEnemy = enemy;
                }
                else
                {
                    if (Vector3.Distance(transform.position, enemy.transform.position) <
                      Vector3.Distance(transform.position, _targetEnemy.transform.position))
                    {
                        _targetEnemy = enemy;
                    }
                }
            }
        }
    }

    private void HandleTargeting()
    {
        _lookForTargetTimer -= Time.deltaTime;
        if (_lookForTargetTimer <= 0)
        {
            _lookForTargetTimer = _lookForTargetTimerMax;
            LookForTargets();
        }
    }

    private void HandleShooting()
    {
        _shootTimer -= Time.deltaTime;
        if(_targetEnemy != null && _shootTimer <= 0)
        {
            ResetShootTimer();
            ArrowProjectile arrowProjectile = UtilsClass.Instantiator<ArrowProjectile>(GameAssets.Instance.pfArrowProjectile, _projectileSpawnPosition.position);
            arrowProjectile.SetTarget(_targetEnemy);
        }        
    }

    private void ResetShootTimer()
    {
        _shootTimer = _shootTimerMax;
    }

}

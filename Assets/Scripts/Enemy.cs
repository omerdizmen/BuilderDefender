using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _lookForTargetTimer;
    private float _lookForTargetTimerMax = 0.2f;
    private HealthSystem _healthSystem;
    private int _damage;
    private Rigidbody2D _rigidBody;
    private Transform _targetTransform;

    private void Awake()
    {
        _healthSystem = GetComponent<HealthSystem>();
        _damage = 10;
        _rigidBody = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        _healthSystem.OnDied += HealthSystem_OnDied;
        _healthSystem.OnDamaged += _healthSystem_OnDamaged;
        if (BuildingManager.Instance.GetHqBuilding != null)
            _targetTransform = BuildingManager.Instance.GetHqBuilding.transform;
        
    }

    private void _healthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        CinemachineShake.Instance.ShakeCamera(7f, 0.12f);
    }

    private void HealthSystem_OnDied(object sender, System.EventArgs e)
    {
        Destroy(gameObject);
    }

    private void Update()
    {

        HandleMovement();
        HandleTargeting();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {        
        Building building = collision.gameObject.GetComponent<Building>();
        
        if(building != null)
        {
            HealthSystem healthSystem = building.GetComponent<HealthSystem>();
            healthSystem.Damage(_damage);
            Destroy(gameObject);
        }
    }

    private void HandleMovement()
    {
        if (_targetTransform)
        {
            Vector2 moveDir = _targetTransform.position - transform.position;
            float moveSpeed = 6f;
            _rigidBody.velocity = moveDir.normalized * moveSpeed;
        }
        else
        {
            _rigidBody.velocity = Vector2.zero;
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
    
    private void LookForTargets()
    {
        float targetRadius = 10f;
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, targetRadius);

        foreach (Collider2D collision in collisions)
        {
            Building building = collision.GetComponent<Building>();
            if(building != null)
            {
                if(_targetTransform == null)
                {
                    _targetTransform = building.transform;
                }
                else
                {
                    if(Vector3.Distance(transform.position, building.transform.position) <
                      Vector3.Distance(transform.position, _targetTransform.position))
                    {
                        _targetTransform = building.transform;
                    }
                }
            }
        }

        if(_targetTransform == null)
        {
            if(BuildingManager.Instance.GetHqBuilding != null)
            _targetTransform = BuildingManager.Instance.GetHqBuilding.transform;
        }
    }
}

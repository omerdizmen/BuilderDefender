using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    private Enemy _targetEnemy;
    private float _timeToDie = 1.2f;
    private Vector3 _lastMoveDirection;
    private void Start()
    {
    }

    private void Update()
    {
        Vector3 moveDir;
        if (_targetEnemy != null)
        {
            moveDir = (_targetEnemy.transform.position - transform.position).normalized;
            _lastMoveDirection = moveDir;
        }
        else
        {
            moveDir = _lastMoveDirection;
        }
        
        


        float moveSpeed = 20f;
        transform.position += moveDir * Time.deltaTime * moveSpeed;
        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(moveDir));

        _timeToDie -= Time.deltaTime;
        if(_timeToDie <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetTarget(Enemy targetEnemy)
    {
        _targetEnemy = targetEnemy;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        if(enemy != null)
        {
            int damageAmount = 10;
            enemy.GetComponent<HealthSystem>().Damage(damageAmount);
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePositionSortingOrder : MonoBehaviour
{
    [SerializeField] private bool _runOnce;
    [SerializeField] private float _positionOffset;


    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        float precisonMultiplier = 5f;
        _spriteRenderer.sortingOrder = (int)(-(transform.position.y + _positionOffset) * precisonMultiplier);

        if (_runOnce)
        {
            Destroy(this);
        }
    }
}

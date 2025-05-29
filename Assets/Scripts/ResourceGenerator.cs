using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    public static int GetNearbyResourceAmount(ResourceGeneratorData resourceGenerator, Vector3 position)
    {
        Collider2D [] collider2DArray = Physics2D.OverlapCircleAll(position, resourceGenerator.resourceDetectionRadius);

        int nearbyResourceAmount = 0;
        foreach (Collider2D collision in collider2DArray)
        {
            ResourceNode resourceNode = collision.GetComponent<ResourceNode>();
            if(resourceNode != null && resourceNode.resourceType == resourceGenerator.resourceType)
            {
                nearbyResourceAmount++;
            }
        }

        nearbyResourceAmount = Mathf.Clamp(nearbyResourceAmount, 0, resourceGenerator.maxResouceAmount);
        
        return nearbyResourceAmount;
    }

    public float timerMax => _timerMax;
    public float timer => _timer;
    public float timerNormalized => _timer / _timerMax;

    private BuildingTypeSO _buildingType;
    private float _timerMax;
    private float _timer;
    private ResourceGeneratorData _resourceGeneratorData;

    private void Awake()
    {
        _resourceGeneratorData = GetComponent<BuildingTypeHolder>().buildingType.resourceGeneratorData;
        _buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        _timerMax = _resourceGeneratorData.timerMax;
    }


    private void Start()
    {
        int nearbyResourceAmount = GetNearbyResourceAmount(_resourceGeneratorData, transform.position);         
        
        if(nearbyResourceAmount == 0)
        {
            enabled = false;
        }
        else
        {
            _timerMax = (_resourceGeneratorData.timerMax / 2f) + _resourceGeneratorData.timerMax * (1 - (float)nearbyResourceAmount / _resourceGeneratorData.maxResouceAmount);
        }
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if(_timer <= 0)
        {
            _timer = _timerMax;
            //Debug.Log("Ding! " + _buildingType.resourceGeneratorData.resourceType.resourceName);
            
            ResourceManager.Instance.AddResource(_resourceGeneratorData.resourceType, 1);
        }
    }

    private void OnDisable()
    {
        Debug.Log("disabled??");
    }

    public ResourceGeneratorData GetResourceGeneratorData()
    {
        return _resourceGeneratorData;
    }

    public float GetAmountGeneratedPerSecond()
    {
        return 1 / _timerMax;
    }


}

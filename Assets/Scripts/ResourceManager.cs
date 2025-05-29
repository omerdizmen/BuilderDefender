using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }
    public event EventHandler OnResourceAmountChanged;

    [SerializeField] private List<ResourceAmount> _startingResoruceAmount;

    private Dictionary<ResourceTypeSO, int> _resourceAmountDictionary;

    private void Awake()
    {
        Instance = this;
        _resourceAmountDictionary = new Dictionary<ResourceTypeSO, int>();
        
        ResourceTypesListSO resourceTypesListSO = Resources.Load<ResourceTypesListSO>(typeof(ResourceTypesListSO).Name);

        foreach (ResourceTypeSO resourceType in resourceTypesListSO.list)
        {
            _resourceAmountDictionary[resourceType] = 0;
        }

        foreach (ResourceAmount resourceAmount in _startingResoruceAmount)
        {
            AddResource(resourceAmount.resourceType, resourceAmount.amount);
        }

    }


    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    ResourceTypesListSO resourceTypesListSO = Resources.Load<ResourceTypesListSO>(typeof(ResourceTypesListSO).Name);
        //    AddResource(resourceTypesListSO.list[0], 10);
        //    TestLogResourceAmount();
        //}
    }

    private void TestLogResourceAmount()
    {
        foreach (ResourceTypeSO resourceType in _resourceAmountDictionary.Keys)
        {
            Debug.Log(resourceType.resourceName + ": " + _resourceAmountDictionary[resourceType]);
        }
    }

    public void AddResource(ResourceTypeSO resourceType, int amount)
    {
        _resourceAmountDictionary[resourceType] += amount;
        OnResourceAmountChanged?.Invoke(this, EventArgs.Empty);
        //TestLogResourceAmount();
    }    

    public int GetResourceAmount(ResourceTypeSO resourceType)
    {
        return _resourceAmountDictionary[resourceType];
    }

    public bool CanAfford(ResourceAmount[] resourceAmountArray)
    {
        foreach (ResourceAmount resourceAmount in resourceAmountArray)
        {
            if (GetResourceAmount(resourceAmount.resourceType) >= resourceAmount.amount)
            {

            }
            else
            {
                return false;
            }
        }

        SpendResources(resourceAmountArray);
        return true;
    }

    private void SpendResources(ResourceAmount[] resourceAmountArray)
    {
        foreach (ResourceAmount resourceAmount in resourceAmountArray)
        {
            _resourceAmountDictionary[resourceAmount.resourceType] -= resourceAmount.amount;
        }
    }
}

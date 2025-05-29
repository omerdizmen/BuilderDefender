using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceNearbyOverlay : MonoBehaviour
{
    private ResourceGeneratorData _resourceGeneratorData;

    private void Awake()
    {
        Hide();
    }

    public void Show(ResourceGeneratorData resourceGeneratorData)
    {
        _resourceGeneratorData = resourceGeneratorData;
        gameObject.SetActive(true);

        transform.Find("icon").GetComponent<SpriteRenderer>().sprite = _resourceGeneratorData.resourceType.sprite;
        
    }

    private void Update()
    {
        
        int nearbyResourceAmount = ResourceGenerator.GetNearbyResourceAmount(_resourceGeneratorData, UtilsClass.GetMouseWorldPosition());
        float percent = Mathf.RoundToInt((float)nearbyResourceAmount / (float)_resourceGeneratorData.maxResouceAmount * 100f);
        transform.Find("text").GetComponent<TextMeshPro>().SetText(percent + "%");
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}

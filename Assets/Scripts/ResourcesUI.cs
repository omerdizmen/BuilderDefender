using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourcesUI : MonoBehaviour
{
    private ResourceTypesListSO _resourceTypeList;
    private Dictionary<ResourceTypeSO, Transform> _resourceTypeTransformDictionary;

    private void Awake()
    {
        _resourceTypeList = Resources.Load<ResourceTypesListSO>(typeof(ResourceTypesListSO).Name);
        _resourceTypeTransformDictionary = new Dictionary<ResourceTypeSO, Transform>();

        Transform resourceTemplate = transform.Find("ResourceTemplate");
        resourceTemplate.gameObject.SetActive(false);

        int index = 0;
        foreach (ResourceTypeSO resourceType in _resourceTypeList.list)
        {
            Transform resourceTransform = Instantiate(resourceTemplate, transform);
            resourceTransform.gameObject.SetActive(true);
            _resourceTypeTransformDictionary[resourceType] = resourceTransform;

            

            resourceTransform.gameObject.SetActive(true);
            float offsetAmount = -160;

            resourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(index * offsetAmount, 0);

            resourceTransform.Find("image").GetComponent<Image>().sprite = resourceType.sprite;

            
            index++;
        }
    }

    private void Start()
    {
        ResourceManager.Instance.OnResourceAmountChanged += ResourceManager_OnResourceAmountChanged;
        UpdateResourceAmount();
    }

    private void ResourceManager_OnResourceAmountChanged(object sender, System.EventArgs e)
    {
        UpdateResourceAmount();
    }

    private void Update()
    {
        
    }

    private void UpdateResourceAmount()
    {
        foreach (ResourceTypeSO resourceType in _resourceTypeList.list)
        {
            int resourceAmount = ResourceManager.Instance.GetResourceAmount(resourceType);

            Transform resourceTransform = _resourceTypeTransformDictionary[resourceType];
            resourceTransform.Find("text").GetComponent<TextMeshProUGUI>().SetText(resourceAmount.ToString());
        }
    }
}

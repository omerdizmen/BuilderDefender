using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceGeneratorOverlay : MonoBehaviour
{
    [SerializeField] private ResourceGenerator _resourceGenerator;

    private Transform _barTransform;

    private void Awake()
    {
        _barTransform = transform.Find("bar");
    }

    private void Start()
    {
        ResourceGeneratorData resourceGeneratorData =  _resourceGenerator.GetResourceGeneratorData();

        transform.Find("icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.sprite;
        transform.Find("bar").localScale = new Vector3(_resourceGenerator.timerNormalized, 1, 1);
        transform.Find("text").GetComponent<TextMeshPro>().SetText(_resourceGenerator.GetAmountGeneratedPerSecond().ToString("F1"));
    }

    private void Update()
    {
        _barTransform.localScale = new Vector3(1 - _resourceGenerator.timerNormalized, 1, 1);
    }
}

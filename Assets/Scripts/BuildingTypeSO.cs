using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="ScriptableObjects/BuildingType")]
public class BuildingTypeSO : ScriptableObject
{
    public bool hasResourceGeneratorData;
    public float minConstruictionRadius;
    public float constructionTimerMax;
    public int healthAmountMax;
    public string buildingName;
    public Transform prefab;
    public ResourceGeneratorData resourceGeneratorData;
    public ResourceAmount[] constructionResourceCostArray;
    public Sprite sprite;

    public string GetConstructionCostString()   
    {
        string str = "";               

        foreach (ResourceAmount resourceAmount  in constructionResourceCostArray)
        {
            str += "<color=#" + resourceAmount.resourceType.colorHex + ">" + resourceAmount.resourceType.nameShort +  resourceAmount.amount + "</color> ";
        }

        return str;
    }
}

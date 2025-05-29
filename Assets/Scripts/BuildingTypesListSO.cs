using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/BuildingTypeList")]
public class BuildingTypesListSO : ScriptableObject
{
    public List<BuildingTypeSO> list;
}

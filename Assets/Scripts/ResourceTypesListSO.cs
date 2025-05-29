using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ResourceTypesList")]
public class ResourceTypesListSO : ScriptableObject
{
    public List<ResourceTypeSO> list;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingTypeSelectUI : MonoBehaviour
{
    [SerializeField] private List<BuildingTypeSO> _buildingTypeList;

    private Dictionary<BuildingTypeSO, Transform> _buildingTypeTransformDictionary;
    private Transform _lastSelected;

    private void Awake()
    {
        _buildingTypeTransformDictionary = new Dictionary<BuildingTypeSO, Transform>();

        Transform btnTemplate = transform.Find("btnTemplate");
        btnTemplate.gameObject.SetActive(false);

        BuildingTypesListSO buildingTypeList = Resources.Load<BuildingTypesListSO>(typeof(BuildingTypesListSO).Name);

        int index = 0;
        foreach (BuildingTypeSO buildingType in buildingTypeList.list)
        {
            if (_buildingTypeList.Contains(buildingType)) continue;

            Transform btnTransform = Instantiate(btnTemplate, transform);
            btnTransform.gameObject.SetActive(true);

            float offset = 250f;
            RectTransform rectTransform = btnTransform.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(index * offset + rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y);

            btnTransform.Find("Image").GetComponent<Image>().sprite = buildingType.sprite;

            Button btn = btnTransform.GetComponent<Button>();
            btn.onClick.AddListener(() =>
            {
                BuildingManager.Instance.SetActiveBuildingType(buildingType);

                UpdateActiveBuildingTypeButton();

            });

            MouseEnterExitEvents mouseEnterExitEvents = btnTransform.GetComponent<MouseEnterExitEvents>();
            mouseEnterExitEvents.OnMouseEnter += (object sender, System.EventArgs e) => 
            {
                TooltipUI.Instance.Show(buildingType.buildingName + "\n" + buildingType.GetConstructionCostString());
            };

            mouseEnterExitEvents.OnMouseExit += (object sender, System.EventArgs e) =>
            {
                TooltipUI.Instance.Hide();
            };

            _buildingTypeTransformDictionary[buildingType] = btnTransform;
            index++;
        }
    }

    private void MouseEnterExitEvents_OnMouseEnter(object sender, System.EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void Start()
    {
        BuildingManager.Instance.OnBuildingPlaced += BuildingManager_OnBuildingPlaced;
    }

    private void BuildingManager_OnBuildingPlaced(object sender, System.EventArgs e)
    {
        _lastSelected.Find("selected").gameObject.SetActive(false);
    }

    private void UpdateActiveBuildingTypeButton()
    {
        if (_lastSelected)
        {
            _lastSelected.Find("selected").gameObject.SetActive(false);
        }

        _lastSelected = _buildingTypeTransformDictionary[BuildingManager.Instance.GetActiveBuildingType()];
        _lastSelected.Find("selected").gameObject.SetActive(true);

    }

}

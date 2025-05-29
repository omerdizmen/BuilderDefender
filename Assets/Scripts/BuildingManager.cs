using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }
    public Building GetHqBuilding => _hqBuilding;
    public event EventHandler OnBuildingPlaced;
    public event EventHandler<OnActiveBuildingTypeChangedEventArgs> OnActiveBuildingTypeChanged;

    public class OnActiveBuildingTypeChangedEventArgs: EventArgs
    {
        public BuildingTypeSO activeBuildingType;
    }

    [SerializeField] private Building _hqBuilding;

    private BuildingTypeSO _activeBuildingType;
    private BuildingTypesListSO _buildingTypesListSO;
    private Camera _mainCamera;

    private bool _enableRadius;

    private void Awake()
    {
        Instance = this;
        _buildingTypesListSO = Resources.Load<BuildingTypesListSO>(typeof(BuildingTypesListSO).Name);
        //_activeBuildingType = _buildingTypesListSO.list[0];
    }

    private void Start()
    {
        _mainCamera = Camera.main;

        _hqBuilding.GetComponent<HealthSystem>().OnDied += HQ_OnDied;
    }

    private void HQ_OnDied(object sender, EventArgs e)
    {
        GameOverUI.Instance.Show();
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            if (_activeBuildingType)
            {                
                if (CanSpawnBuilding(_activeBuildingType, UtilsClass.GetMouseWorldPosition(), out string errorMessage)
                && ResourceManager.Instance.CanAfford(_activeBuildingType.constructionResourceCostArray)                )
                {
                    //Instantiate(_activeBuildingType.prefab, UtilsClass.GetMouseWorldPosition(), Quaternion.identity);
                    BuildingConstruction.Create(UtilsClass.GetMouseWorldPosition(), _activeBuildingType);
                    SetActiveBuildingTypeToNull();
                    OnBuildingPlaced?.Invoke(this, EventArgs.Empty);
                    SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingPlaced);
                }
                else
                {
                    TooltipUI.Instance.Show(errorMessage, new TooltipUI.TooltipTimer { timer = 2.0f });
                }
            }
        }

        if (Input.GetKey(KeyCode.T))
        {
            _enableRadius = !_enableRadius;
        }

        if (_enableRadius)
        {
            Debug.DrawLine(UtilsClass.GetMouseWorldPosition(), UtilsClass.GetMouseWorldPosition() + Vector3.up * 7);
            Debug.DrawLine(UtilsClass.GetMouseWorldPosition(), UtilsClass.GetMouseWorldPosition() + Vector3.down * 7);
            Debug.DrawLine(UtilsClass.GetMouseWorldPosition(), UtilsClass.GetMouseWorldPosition() + Vector3.right * 7);
            Debug.DrawLine(UtilsClass.GetMouseWorldPosition(), UtilsClass.GetMouseWorldPosition() + Vector3.left * 7);
        }


    }


    public void SetActiveBuildingType(BuildingTypeSO buildingType)
    {
        _activeBuildingType = buildingType;
        OnActiveBuildingTypeChanged?.Invoke(this, new OnActiveBuildingTypeChangedEventArgs
        {
            activeBuildingType = _activeBuildingType
        }); ;
    }

    public BuildingTypeSO GetActiveBuildingType()
    {
        return _activeBuildingType;
    }

    private void SetActiveBuildingTypeToNull()
    {
        _activeBuildingType = null;
        OnActiveBuildingTypeChanged?.Invoke(this, new OnActiveBuildingTypeChangedEventArgs
        {
            activeBuildingType = _activeBuildingType
        }); ;
    }

    private bool CanSpawnBuilding(BuildingTypeSO buildingType, Vector3 position, out string errorMessage)
    {

        BoxCollider2D collider = buildingType.prefab.GetComponent<BoxCollider2D>();

        Collider2D[] collisions = Physics2D.OverlapBoxAll(position + (Vector3)collider.offset, collider.size, 0);

        bool isAreaClear = collisions.Length == 0;
        if (!isAreaClear)
        {
            errorMessage = "Area is not buildable";
            return false;
        }




        collisions = Physics2D.OverlapCircleAll(position, buildingType.minConstruictionRadius);
        foreach (Collider2D collision in collisions)
        {
            // colliders inside the construction radius
            BuildingTypeHolder building = collision.GetComponent<BuildingTypeHolder>();
            if(building != null && building.buildingType == buildingType)
            {
                errorMessage = "Cannot place same building type too close";
                Debug.Log("giriyor");
                return false; 
            }
        }

        float maximumRadius = 25f;

        collisions = Physics2D.OverlapCircleAll(position, maximumRadius);
        foreach (Collider2D collision in collisions)
        {
            BuildingTypeHolder buildingTypeHolder = collision.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder)
            {
                errorMessage = "";
                
                return true;
            }
        }

        errorMessage = "No building nearby";
        return false;
    }
}

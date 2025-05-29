using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingConstruction : MonoBehaviour
{
    public static BuildingConstruction Create(Vector3 position, BuildingTypeSO buildingType)
    {
        BuildingConstruction buildingConstruction = UtilsClass.Instantiator<BuildingConstruction>(GameAssets.Instance.pfBuildingConstruction, position);
        buildingConstruction.SetBuildingType(buildingType);
        return buildingConstruction;
    }
    public float getConstructionTimerNormalized => 1 - _constructionTimer / _constructionTimerMax;

    private BuildingTypeSO _buildingType;
    private BuildingTypeHolder _buildingTypeHolder;
    private BoxCollider2D _boxCollider;
    private float _constructionTimer;
    private float _constructionTimerMax;
    private Material _constructionMaterial;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _spriteRenderer = transform.Find("sprite").GetComponent<SpriteRenderer>();
        _buildingTypeHolder = GetComponent<BuildingTypeHolder>();
        _constructionMaterial = _spriteRenderer.material;
    }

    private void Update()
    {
        _constructionTimer -= Time.deltaTime;

        _constructionMaterial.SetFloat("_Progress", getConstructionTimerNormalized);
        if(_constructionTimer <= 0)
        {
            Debug.Log("Dongo");
            Instantiate(_buildingType.prefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void SetBuildingType(BuildingTypeSO buildingType)
    {
        _buildingType = buildingType;
        _constructionTimerMax = buildingType.constructionTimerMax;
        _constructionTimer = _constructionTimerMax;

        _spriteRenderer.sprite = buildingType.sprite;
        _boxCollider.offset = buildingType.prefab.GetComponent<BoxCollider2D>().offset;
        _boxCollider.size = buildingType.prefab.GetComponent<BoxCollider2D>().size;

        _buildingTypeHolder.buildingType = buildingType;
    }

}

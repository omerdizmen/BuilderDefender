using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private event System.EventHandler OnHideTimeEnd;

    private float _hideTime;
    private float _hideTimeMax;
    private HealthSystem _healthSystem;
    private Transform _barTransform;
    private Transform _seperatorContainer;
    private Transform _barBackground;
    

    private void Awake()
    {

        _hideTimeMax = 3;

        _healthSystem = transform.root.GetComponent<HealthSystem>();
        _barTransform = transform.Find("bar");

        OnHideTimeEnd += HealthBar_OnHideTimeEnd;
        
    }

    

    private void HealthBar_OnHideTimeEnd(object sender, System.EventArgs e)
    {
        //SetActive(false);
    }

    private void Start()
    {
        _seperatorContainer = transform.Find("seperatorContainer");
        _barBackground = transform.Find("barBackground");
        Transform seperatorTemplate = _seperatorContainer.Find("seperatorTemplate");
        seperatorTemplate.gameObject.SetActive(false);

        float barLength = 3f;
        float barMonkey = barLength / _healthSystem.getHealthAmountMax;
        int healthSeperatorCount = Mathf.FloorToInt(_healthSystem.getHealthAmountMax / 10);
        float barDistance = barLength / healthSeperatorCount;        

        for (int i = 1; i < healthSeperatorCount; i++)
        {
            Transform seperatorTransform = Instantiate(seperatorTemplate, _seperatorContainer);
            seperatorTransform.gameObject.SetActive(true);
            seperatorTransform.localPosition = new Vector3(_barBackground.localPosition.x + barDistance * i, seperatorTransform.localPosition.y, 0);
            //seperatorTransform.localPosition = new Vector3(barMonkey * i * 10, seperatorTransform.localPosition.y, 0);
        }

        _healthSystem.OnDamaged += HealthSystem_OnDamaged;
        UpdateBar();
        //UpdateHealthBarVisible();
    }

    private void Update()
    {
        _hideTime -= Time.deltaTime;

        if(_hideTime <= 0)
        {            
            //OnHideTimeEnd?.Invoke(this, System.EventArgs.Empty);
        }
    }


    private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        SetActive(true);
        UpdateBar();
        UpdateHealthBarVisible();
        SetHideTimeToMax();
    }

    private void UpdateBar()
    {
        float healthAmountNormalized = _healthSystem.getHealthAmountNormalized;       
        _barTransform.localScale = new Vector3(healthAmountNormalized, 1, 1);
    }

    private void UpdateHealthBarVisible()
    {
        if (_healthSystem.isFullHealth)
        {
            SetActive(false);
        }
    }

    private void SetHideTimeToMax()
    {
        _hideTime = _hideTimeMax;
    }

    private void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilsClass 
{
    private static Camera _mainCamera;

    public static  Vector3 GetMouseWorldPosition()
    {
        if(_mainCamera == null)
        {
            _mainCamera = Camera.main;
        }

        Vector3 mouseWorldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;

        return mouseWorldPosition;
    }

    public static Vector3 GetRandomDirection()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    public static T Instantiator<T>(Transform obj, Vector3 position) where T : Object
    {
        Transform sourceTransform = GameObject.Instantiate(obj, position, Quaternion.identity);

        T instantiatedObject = sourceTransform.GetComponent<T>();

        return instantiatedObject;
    }

    public static float GetAngleFromVector(Vector3 vector)
    {
        float radians = Mathf.Atan2(vector.y, vector.x);
        float degrees = radians * Mathf.Rad2Deg;

        return degrees;
    }
}

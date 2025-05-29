using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;

    private float _orthographicSize;
    private float _targetOrthographicSize;

    private float _minumumOrthographicSize;
    private float _maximumOrthographicSize;

    private bool _edgeScrolling;

    private void Awake()
    {
        _minumumOrthographicSize = 10f;
        _maximumOrthographicSize = 20f;

        _edgeScrolling = PlayerPrefs.GetInt("edgeScrolling", 1) == 1;
    }

    private void Start()
    {
        _orthographicSize = _cinemachineVirtualCamera.m_Lens.OrthographicSize;
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        //float edgeScrollingSize = 50;

        //if(Input.mousePosition.x > Screen.width - edgeScrollingSize)
        //{
        //    x = 1;
        //}

        //if (Input.mousePosition.x < edgeScrollingSize)
        //{
        //    x = -1;
        //}

        //if (Input.mousePosition.y > Screen.height - edgeScrollingSize)
        //{
        //    y = 1;
        //}

        //if (Input.mousePosition.y < edgeScrollingSize)
        //{
        //    y = -1;
        //}

        Vector2 moveDir = new Vector2(x, y).normalized;
        float moveSpeed = 15f;

        transform.position += (Vector3)moveDir * moveSpeed * Time.deltaTime;

        float zoomAmount = 2f;
        _targetOrthographicSize -= Input.mouseScrollDelta.y * zoomAmount;
        _targetOrthographicSize = Mathf.Clamp(_targetOrthographicSize, _minumumOrthographicSize, _maximumOrthographicSize);

        float zoomSpeed = 5.0f;
        _orthographicSize = Mathf.Lerp(_orthographicSize, _targetOrthographicSize, Time.deltaTime * zoomSpeed);
        _cinemachineVirtualCamera.m_Lens.OrthographicSize = _orthographicSize;
    }

    public void SetEdgeScrolling(bool edgeScrolling)
    {
        _edgeScrolling = edgeScrolling;
        PlayerPrefs.SetInt("edgeScrolling", _edgeScrolling ? 1 : 0);
    }
}

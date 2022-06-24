using System;
using UnityEngine;

public class CarScanner : AScanner
{
    [SerializeField] private float _timeToScan;
    private float _scanTimer;

    private void Update()
    {
        try
        {
            _scanTimer += Time.deltaTime;

            if (_scanTimer >= _timeToScan)
            {
                Ray ray = mainCamera.ScreenPointToRay(mainCamera.WorldToScreenPoint(transform.position));
                RaycastScan(ray);
            }
        }
        catch(Exception ex)
        {
            _scanTimer = 0;
            Debug.LogWarning(ex.Message);
        }
    }
}
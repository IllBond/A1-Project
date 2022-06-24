using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheet : MonoBehaviour
{
    string cheet = "";

    float time;
    float timeer = 5;

    private const string _firstCheet = "00110101";
    private const string _secontCheet = "00111111";

    public void SetFalse() 
    {
        cheet += 0.ToString();
        ResetTimer();
        Debug.Log(cheet);
    }

    public void SetTrue()
    {
        cheet += 1.ToString();
        ResetTimer();
        Debug.Log(cheet);
    }

    private void ResetTimer() 
    {
        time = 0;
    }

    private void ResetCheet() 
    {
        cheet = "";
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (cheet == _firstCheet)
        {
            MainPlayer.Instance.Money = 5000;
            MainPlayer.Instance.ShowMessage("+5000 денег");
            ResetCheet();
        }
                
        
        if (cheet == _secontCheet)
        {
            MainPlayer.Instance.ShowMessage("—брос сохранений");
            SaveManager.DeleteSave();
            ResetCheet();
        }


        if (time >= timeer)
        {

            ResetCheet();
        }

    }
}

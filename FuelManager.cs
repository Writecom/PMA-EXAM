using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelManager : MonoBehaviour
{

    public static FuelManager instance;

    // tutorial: https://www.youtube.com/watch?v=BLfNP4Sc_iA
    public float maxFuel = 100;
    public float _currentFuel;
    public float fuelDepletionRate = 1f;
    public float fuelDepletionAmount = 1f;

    public FuelManager fuelBar;
    public Slider slider;
    private bool callOnce; 
    
    //a get set for the current fuel
    public float CurrentFuel
    {
        get
        {
            return _currentFuel;
        }
        set
        {
            if (value < 0)
            {
                _currentFuel = 0;
                Debug.Log("Fuel can't go below 0");
            }
            else if (value > maxFuel)
            {
                _currentFuel = maxFuel;
                Debug.Log("Fuel can't exceed 100");
            }
            else
            {
                _currentFuel = value;
                slider.value = _currentFuel;
            }
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        callOnce = true;
        CurrentFuel = maxFuel;
        InvokeRepeating("DepleteFuel", 1f, fuelDepletionRate);
    }

    private void FixedUpdate()
    {
        if (CurrentFuel <= 0 && callOnce)
        {
            GameManager.instance.GameOver();
            AudioManageryTest.instance.PlayLoseSound("Engine Bust");
            CancelInvoke("DepleteFuel");
            callOnce = false;
            AudioManageryTest.instance.StopSiren("Siren");
        }
        
    }

    public void DepleteFuel()
    {
        CurrentFuel -= fuelDepletionAmount;
    }

    public void AddFuel(int amount)
    {
        CurrentFuel += amount;
    }

}

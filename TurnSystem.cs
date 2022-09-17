using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    //Singleton
    public static TurnSystem Instance {get; private set;}
    //Events
    public event EventHandler OnTurnChanged;
    //Fields
    private int turnNumber=1;
    private bool playerTurn=true;
    private void Awake()
    {
        if(Instance!=null)
        {
            Debug.Log("TurnSystem singleton instance already exists");
            Destroy(gameObject);
            return;
        }
        Instance=this;
    }
    public bool IsPlayerTurn()
    {
        return playerTurn;
    }
    public int GetTurnNumber()
    {
        return turnNumber;
    }
    public void NextTurn()
    {
        playerTurn=!playerTurn;
        turnNumber++;
        OnTurnChanged?.Invoke(this, EventArgs.Empty);
    }
}

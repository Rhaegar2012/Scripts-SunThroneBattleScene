using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private int turnsTaken=0;
    private int playerAttrition=0;
    private int enemyAttrition=0;
    private float turnRatio;
    private float playerAttritionRatio;
    private float enemyAttritionRatio;
    private float tacticsSliderScore;
    private float dominanceSliderScore;
    private float agilitySliderScore;
    [Header("Agility Metrics")]
    [SerializeField] private  int maxTurnsForBattle;
    [Header("Tactics Metrics")]
    [SerializeField] private int playerUnitsDestroyed;
    [Header("Dominance Metrics")]
    [SerializeField] private int enemyUnitsDestroyed;
    // Start is called before the first frame update
    //Events
    public static event EventHandler<PlayerScore> OnLevelFinished;
 
    void Start()
    {
        UnitManager.Instance.OnArmyDestroyed+=UnitManager_OnArmyDestroyed;
        TurnSystem.Instance.OnTurnChanged+=TurnSystem_OnTurnChanged;
        UnitHealthSystem.OnAnyUnitDestroyed+=UnitHealthSystem_OnAnyUnitDestroyed;
        Target.OnTargetCaptured+=Target_OnTargetCaptured;
    }

    public void Target_OnTargetCaptured(object sender, EventArgs empty)
    {
        EndLevel("Enemy");
    }
    public void UnitManager_OnArmyDestroyed(object sender, string army)
    {
        Debug.Log("End Level event called");
        EndLevel(army);
    }
    public void TurnSystem_OnTurnChanged(object sender, EventArgs empty)
    {
        if(TurnSystem.Instance.IsPlayerTurn())
        {
            turnsTaken++;
        }
    }
    public void UnitHealthSystem_OnAnyUnitDestroyed(object sender, Unit unit)
    {
        if(unit.IsEnemy())
        {
            enemyAttrition++;

        }
        else
        {
            playerAttrition++;
        }
    }
    private void EndLevel(string army)
    {
        if(army=="Enemy")
        {
            CalculateTacticsScore();
            CalculateAgilityScore();
            CalculateDominanceScore();
            PlayerScore playerScore= new PlayerScore(tacticsSliderScore,dominanceSliderScore,agilitySliderScore);
            OnLevelFinished?.Invoke(this, playerScore);
        }
        if(army=="Player")
        {
            GameOverScreen.Open();
        }

    }
    private void CalculateTacticsScore()
    {
        float attritionRatio=(float)playerAttrition/(float)playerUnitsDestroyed;
        if(attritionRatio<0.2f)
        {
            tacticsSliderScore=1f;
        }
        else if(attritionRatio>0.2f && attritionRatio<0.7f)
        {
            tacticsSliderScore=0.5f;
        }
        else if(attritionRatio>0.7f)
        {
            tacticsSliderScore=0.25f;
        }
      
    }
    private void CalculateAgilityScore()
    {

        float turnRatio=(float)turnsTaken/(float)maxTurnsForBattle;
        if(turnRatio<0.2f)
        {
            agilitySliderScore=1f;
        }
        else if(turnRatio>0.2f && turnRatio<0.7f)
        {
            agilitySliderScore=0.5f;
        }
        else if(turnRatio>0.7f)
        {
            agilitySliderScore=0.25f;
        }

    }
    private void CalculateDominanceScore()
    {
      
        float dominanceRatio= (float)enemyAttrition/(float)enemyUnitsDestroyed;
        if(dominanceRatio>0.75f)
        {
            dominanceSliderScore=1f;
        }
        else if(dominanceRatio>0.2f && dominanceRatio<0.75f)
        {
            dominanceSliderScore=0.5f;
        }
        else if(dominanceRatio<0.2f)
        {
            dominanceSliderScore=0.25f;
        }

    }


}

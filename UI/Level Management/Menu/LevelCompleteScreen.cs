using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteScreen : Menu<LevelCompleteScreen>
{
    [SerializeField] private Image tacticsSliderImage;
    [SerializeField] private Image dominanceSliderImage;
    [SerializeField] private Image agilitySliderImage;
    [SerializeField] private Image medalImage;
    [SerializeField] private Sprite bronzeMedalSprite;
    [SerializeField] private Sprite silverMedalSprite;
    [SerializeField] private Sprite goldMedalSprite;
    public override void SubscribeToEvents ()
    {
        Debug.Log("Accessed subscription method");
        BattleManager.OnLevelFinished+=BattleManager_OnLevelFinished;

    }
    public  void UpdateScoreSlider(float tacticsScore, float agilityScore, float dominanceScore)
    {
        tacticsSliderImage.fillAmount=tacticsScore;
        agilitySliderImage.fillAmount=agilityScore;
        dominanceSliderImage.fillAmount=dominanceScore;

    }

    public  void DisplayRankBadge(float tacticsScore, float agilityScore, float dominanceScore)
    {
        float[] scores={tacticsScore,agilityScore,dominanceScore};
        float averageScore= scores.AsQueryable().Average();
        if(averageScore>0.85)
        {
            medalImage.sprite=goldMedalSprite;
        }
        else if(averageScore>=0.5 && averageScore<=0.85)
        {
            medalImage.sprite=silverMedalSprite;
        }
        else if(averageScore<=0.5)
        {
            medalImage.sprite=bronzeMedalSprite;
        }
    }
    public void BattleManager_OnLevelFinished(object sender,PlayerScore scores)
    {
        UpdateScoreSlider(scores.TacticsScore,scores.AgilityScore,scores.DominanceScore);
        DisplayRankBadge(scores.TacticsScore,scores.AgilityScore,scores.DominanceScore);
        Open();
    }

    
}

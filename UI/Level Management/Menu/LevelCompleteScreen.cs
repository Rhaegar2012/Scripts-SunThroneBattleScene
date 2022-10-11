using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteScreen : Menu<LevelCompleteScreen>
{
    [SerializeField] private static Image tacticsSliderImage;
    [SerializeField] private static Image dominanceSliderImage;
    [SerializeField] private static Image agilitySliderImage;
    public static void UpdateScoreSlider(float tacticsScore, float agilityScore, float dominanceScore)
    {
        tacticsSliderImage.fillAmount=tacticsScore;
        agilitySliderImage.fillAmount=agilityScore;
        dominanceSliderImage.fillAmount=dominanceScore;

    }

    public static void DisplayPlayerRank(float tacticsScore, float agilityScore, float dominanceScore)
    {
        //TODO
    }

    
}

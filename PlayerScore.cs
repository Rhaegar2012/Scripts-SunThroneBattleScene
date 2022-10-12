using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore
{
    private float tacticsScore;
    private float dominanceScore;
    private float agilityScore;
    public float TacticsScore{get{return tacticsScore;}set{tacticsScore=value;}}
    public float DominanceScore{get{return dominanceScore;}set{dominanceScore=value;}}
    public float AgilityScore{get{return agilityScore;}set{agilityScore=value;}}

    public PlayerScore(float tacticsScore, float dominanceScore,float agilityScore)
    {
        this.tacticsScore=tacticsScore;
        this.dominanceScore=dominanceScore;
        this.agilityScore=agilityScore;
    }


}

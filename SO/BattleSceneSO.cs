using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Battle Data Menu", fileName="New Battle")]
public class BattleSceneSO : ScriptableObject
{

    [Header("Player Army")]
    [SerializeField] private List<UnitType> playerArmyUnits;
    [SerializeField] private List<Vector2> playerArmyPositions;
    [Header("Enemy Army")]
    [SerializeField] private List<UnitType> enemyArmyUnits;
    [SerializeField] private List<Vector2> enemyArmyPositions;

    public void GetBattleUnits(out List<UnitType> playerUnits,out List<UnitType> enemyUnits)
    {
         playerUnits=playerArmyUnits; 
         enemyUnits=enemyArmyUnits;
    }

    public void GetBattleStartingPositions(out List<Vector2> playerPositions,out List<Vector2> enemyPositions )
    {
         playerPositions=playerArmyPositions; 
         enemyPositions=enemyArmyPositions;
    }
}

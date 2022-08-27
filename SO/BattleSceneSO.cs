using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Battle Data Menu", fileName="New Battle")]
public class BattleSceneSO : ScriptableObject
{

    [Header("Player Army")]
    [SerializeField] List<UnitType> playerArmyUnits;
    [SerializeField] List<Vector2> playerArmyPositions;
    [Header("Enemy Army")]
    [SerializeField] List<UnitType> enemyArmyUnits;
    [SerializeField] List<Vector2> enemyArmyPositions;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameOver())
        {
            //TODO
        }
        if(LevelComplete())
        {
            //TODO
        }

    }
    private bool LevelComplete()
    {
        List<Unit> enemyUnits=UnitManager.Instance.GetEnemyUnitList();
        if(enemyUnits.Count<=0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool GameOver()
    {
        List<Unit> friendlyUnits=UnitManager.Instance.GetFriendlyUnitList();
        if(friendlyUnits.Count<=0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

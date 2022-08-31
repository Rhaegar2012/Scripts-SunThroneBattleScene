using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    //Singleton
    public static UnitManager Instance {get; private set;}
    //Fields
    [SerializeField] Transform unitPrefab;
    [SerializeField] BattleSceneSO battleSO;
    [Header("Unit Sprites-Player")]
    [SerializeField] Sprite playerInfantrySprite;
    [SerializeField] Sprite playerLightArmorSprite;
    [SerializeField] Sprite playerMBTSprite;
    [Header("Unit Sprites-Enemy")]
    [SerializeField] Sprite enemyInfantrySprite;
    [SerializeField] Sprite enemyLightArmorSprite;
    [SerializeField] Sprite enemyMBTSprite;

    private List<Unit> unitList;
    private List<Unit> friendlyUnitList;
    private List<Unit> enemyUnitList;
    private void Awake()
    {
        if(Instance!=null)
        {
            Debug.Log("UnitManager Singleton already exists");
            Destroy(gameObject);
            return;
        }
        Instance=this;
        unitList= new List<Unit>();
        friendlyUnitList=new List<Unit>();
        enemyUnitList= new List<Unit>();
    }
    // Start is called before the first frame update
    void Start()
    {
        List<UnitType> playerUnitTypeList= new List<UnitType>();
        List<UnitType> enemyUnitTypeList= new List<UnitType>();
        List<Vector2> playerUnitPositions = new List<Vector2>();
        List<Vector2> enemyUnitPositions = new List<Vector2>();
        battleSO.GetBattleUnits(out playerUnitTypeList, out enemyUnitTypeList);
        battleSO.GetBattleStartingPositions(out playerUnitPositions, out enemyUnitPositions);
        //Place Player Units
        if(playerUnitTypeList.Count>0 && playerUnitPositions.Count>0 &&playerUnitTypeList.Count==playerUnitPositions.Count)
            {
                for(int i=0;i<playerUnitTypeList.Count;i++)
                {
                    UnitType unitType= playerUnitTypeList[i];
                    GridNode unitGridNode= LevelGrid.Instance.GetNodeAtPosition(playerUnitPositions[i]);
                    Transform newUnit= Instantiate(unitPrefab,unitGridNode.GetGridPosition(),Quaternion.identity);
                    Unit unitScript= newUnit.GetComponent<Unit>(); 
                    switch(unitType)
                    {
                        case UnitType.Infantry:
                        unitScript.SetUnitParameters(1,1,5,unitType,false,unitGridNode,playerInfantrySprite);
                        break;
                        case UnitType.LightArmor:
                        unitScript.SetUnitParameters(5,5,7,unitType,false,unitGridNode,playerLightArmorSprite);
                        break;
                        case UnitType.MBT:
                        unitScript.SetUnitParameters(5,5,7,unitType,false,unitGridNode,playerMBTSprite);
                        break;
                    }
                    friendlyUnitList.Add(unitScript);
                    unitList.Add(unitScript);
                    LevelGrid.Instance.SetUnitAtGridNode(unitGridNode.GetGridPosition(),unitScript);
                    
                }
            }
        //Place Enemy Units
        if(enemyUnitTypeList.Count>0 && enemyUnitPositions.Count>0 &&enemyUnitTypeList.Count==enemyUnitPositions.Count)
            {
                for(int i=0;i<enemyUnitTypeList.Count;i++)
                {
                    UnitType unitType= enemyUnitTypeList[i];
                    GridNode unitGridNode= LevelGrid.Instance.GetNodeAtPosition(enemyUnitPositions[i]);
                    Transform newUnit= Instantiate(unitPrefab,unitGridNode.GetGridPosition(),Quaternion.identity);
                    Unit unitScript= newUnit.GetComponent<Unit>(); 
                    switch(unitType)
                    {
                        case UnitType.Infantry:
                        unitScript.SetUnitParameters(1,1,5,unitType,true,unitGridNode,enemyInfantrySprite);
                        break;
                        case UnitType.LightArmor:
                        unitScript.SetUnitParameters(5,5,7,unitType,true,unitGridNode,enemyLightArmorSprite);
                        break;
                        case UnitType.MBT:
                        unitScript.SetUnitParameters(5,5,7,unitType,true,unitGridNode,enemyMBTSprite);
                        break;
                    }
                    enemyUnitList.Add(unitScript);
                    unitList.Add(unitScript);
                    LevelGrid.Instance.SetUnitAtGridNode(unitGridNode.GetGridPosition(),unitScript);
                }
            }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

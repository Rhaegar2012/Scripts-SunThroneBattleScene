using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public enum TargetType
{
    HQ=0,
    Bonus=1
}
public class Target : MonoBehaviour
{
    private Vector2 targetGridPosition;
    private TargetType targetType;
    private bool isCaptured;
    private SpriteRenderer spriteRenderer;
    private Color playerColor=Color.blue;
    private Color enemyColor=Color.red;
    private Unit captureUnit;
    [SerializeField] private int captureActionsRequired;
    [SerializeField] private TextMeshProUGUI turnsToCaptureText;
    private void Awake()
    {
        UpdateTurnsToCaptureText();
        spriteRenderer=GetComponentInChildren<SpriteRenderer>();
    }
    public void ExecuteCapture()
    {
        captureActionsRequired--;
        UpdateTurnsToCaptureText();
    }
    public bool IsTargetCaptured()
    {
        if(captureActionsRequired<0)
        {
            return true;
        }
        return false;

    }
   
    public void SetTargetGridPosition(Vector2 targetGridPosition)
    {
        this.targetGridPosition=targetGridPosition;
    }
    public Vector2 GetTargetGridPosition()
    {
        return targetGridPosition;
    }

    private void UpdateTurnsToCaptureText()
    {
        turnsToCaptureText.text=captureActionsRequired.ToString();
    }
    private void UpdateCaptureColor()
    {
        if(captureUnit.IsEnemy())
        {

        }


    }

}

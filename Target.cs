using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    [SerializeField] private int captureActionsRequired;
    public void ExecuteCaptureAction()
    {
        captureActionsRequired--;
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

}

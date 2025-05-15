using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "Lign_Of_Sight_Check", story: "Check [Target] with [LineofSightDetector]", category: "Conditions", id: "bb896dcfe4546f975b93d9ef4399b928")]
public partial class LignOfSightCheckCondition : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    private Transform TargetTransform;
    [SerializeReference] public BlackboardVariable<Line_Of_Sight_Detector> LineofSightDetector;
    [SerializeField] private GameObject _target;

    public override bool IsTrue()
    {
        TargetTransform = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        _target = TargetTransform.gameObject.transform.parent.gameObject;
        Debug.Log("Target: " + _target);
        return LineofSightDetector.Value.Detection();
    }


}

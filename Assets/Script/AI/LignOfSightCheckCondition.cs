using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "Lign_Of_Sight_Check", story: "Check [Target] with [LineofSightDetector]", category: "Conditions", id: "bb896dcfe4546f975b93d9ef4399b928")]
public partial class LignOfSightCheckCondition : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<Line_Of_Sight_Detector> LineofSightDetector;

    public override bool IsTrue()
    {
        return LineofSightDetector.Value.Detection(Target.Value) != null;
    }


}

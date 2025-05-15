using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Range", story: "Update [range] and assign [Target]", category: "Action", id: "d0cc66d97d5cff94d152c0283d850a04")]
public partial class RangeAction : Action
{
    [SerializeReference] public BlackboardVariable<Range> Range;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    protected override Status OnUpdate()
    {
        Target.Value = Range.Value.UpdateDetector();
        return Range.Value.UpdateDetector() == null ? Status.Failure : Status.Success;
    }


}


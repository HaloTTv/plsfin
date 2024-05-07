using System;
using UnityEngine;
using Unity.Muse.Behavior;
using Action = Unity.Muse.Behavior.Action;

[Serializable]
[NodeDescription(name: "Listen  For  Click", story: "Wait for a specified mouse button to be clicked", category: "Action", id: "3df2b2c9b0fbb749da95ef8aa9787924")]
public class ListenForClick : Action
{

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}


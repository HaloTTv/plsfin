// 2024-05-06 AI-Tag
// This was created with assistance from Muse, a Unity Artificial Intelligence product.
using System;
using UnityEngine;
using Unity.Muse.Behavior;
using Action = Unity.Muse.Behavior.Action;

[Serializable]
[NodeDescription(name: "Sword Swing", story: "Swing a sword", category: "Action/Animation", id: "056dbbfb008ec1d2a291b10d4f620b1c")]
public class SwordSwing : Action
{
    private BlackboardVariable<Animator> animatorVariable;

    protected override Status OnStart()
    {
        if (!animatorVariable.Value)
        {
            return Status.Failure;
        }
        animatorVariable.Value.SetTrigger("SwordSwing");
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        AnimatorStateInfo stateInfo = animatorVariable.Value.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("SwordSwing") && stateInfo.normalizedTime < 1.0f)
        {
            return Status.Running;
        }
        else
        {
            return Status.Success;
        }
    }

    protected override void OnEnd()
    {
        animatorVariable.Value.ResetTrigger("SwordSwing");
    }
}
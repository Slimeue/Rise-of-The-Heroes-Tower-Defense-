using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{

    public event Action OnDeathFinish;
    public event Action OnAttackEnd;

    private void AnimationDeathFinishedTrigger()
    {
        OnDeathFinish?.Invoke();
    }

    private void AnimationAttackFinishedTrigger()
    {
        OnAttackEnd?.Invoke();
    }



}

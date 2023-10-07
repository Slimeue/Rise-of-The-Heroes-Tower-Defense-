using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeCharAnimationHandler : MonoBehaviour
{

    public event Action OnRangeFinishAttack;
    public event Action OnRangeStartAttack;

    private void AnimationRangeFinishAttack()
    {
        OnRangeFinishAttack?.Invoke();//finish attack - > able to spawn bullet again
    }

    private void AnimtionRangeStartAttack()
    {
        OnRangeStartAttack?.Invoke();//spawn bullet
    }
}

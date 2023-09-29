using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{

    public event Action OnFinish;

    private void AnimationDeathFinishedTrigger()
    {
        OnFinish?.Invoke();
    }



}

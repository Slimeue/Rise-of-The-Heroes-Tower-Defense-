using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{

    public event Action OnDeathFinish;
    public event Action OnAttackEnd;
    public event Action OnSkillActivated;
    public event Action OnSkillFinished;
    public event Action OnAnimationTrigger;

    private void AnimationSoundTrigger()
    {
        OnAnimationTrigger?.Invoke();
    }

    private void AnimationDeathFinishedTrigger()
    {
        OnDeathFinish?.Invoke();
    }

    private void AnimationAttackFinishedTrigger()
    {
        OnAttackEnd?.Invoke();
    }

    private void AnimationSkillTrigger()
    {
        OnSkillActivated?.Invoke();
    }
    private void AnimationSkilLFinished()
    {
        OnSkillFinished?.Invoke();
    }




}

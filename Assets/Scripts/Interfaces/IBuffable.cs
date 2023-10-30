using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuffable
{
    public void AttackSpeedBuff(float percentage, float duration);//buff attackSpeed

    public void AttackBuff(float percentage, float duration);//buff attack

}

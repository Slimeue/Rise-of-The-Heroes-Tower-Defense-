using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDebuffable
{
    void IncreaseDamageTaken(float damageMultiplier, float duration);

    bool IsDebuff();

}

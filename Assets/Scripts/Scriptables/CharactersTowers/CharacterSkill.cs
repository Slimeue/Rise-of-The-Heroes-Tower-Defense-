using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Charaters/Skill", menuName = "Character/Skill Data")]
public class CharacterSkill : ScriptableObject
{

    public Sprite skillArtWork;
    public string skillName;
    public string skillType;

    [TextArea(1, 10)]
    public string skillDescription;

}

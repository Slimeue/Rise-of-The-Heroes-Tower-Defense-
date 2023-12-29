using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Charaters/Towers", menuName = "Character/Character Info Data")]
public class CharacterInfoData : ScriptableObject
{
    [SerializeField][TextArea(5, 10)] public string lorePart1;
    [SerializeField][TextArea(5, 10)] public string lorePart2;
    [SerializeField][TextArea(5, 10)] public string lorePart3;

    [SerializeField][TextArea(5, 10)] public string personalityPart1;
    [SerializeField][TextArea(5, 10)] public string personalityPart2;
    [SerializeField][TextArea(5, 10)] public string personalityPart3;


    [SerializeField][TextArea(3, 5)] public string[] charTrivias;

}

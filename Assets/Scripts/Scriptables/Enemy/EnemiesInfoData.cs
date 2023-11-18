using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemies", menuName = "Enemy/Enemy Info Data")]

public class EnemiesInfoData : ScriptableObject
{
    [SerializeField][TextArea(5, 10)] public string lorePart1;
    [SerializeField][TextArea(5, 10)] public string lorePart2;
    [SerializeField][TextArea(5, 10)] public string lorePart3;

    [SerializeField][TextArea(5, 10)] public string personalityPart1;
    [SerializeField][TextArea(5, 10)] public string personalityPart2;
    [SerializeField][TextArea(5, 10)] public string personalityPart3;
}

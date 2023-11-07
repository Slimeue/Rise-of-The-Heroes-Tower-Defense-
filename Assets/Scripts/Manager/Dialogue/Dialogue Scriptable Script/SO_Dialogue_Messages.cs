using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Conversation", menuName = "Dialogue/Conversation")]
public class SO_Dialogue_Messages : ScriptableObject
{
    public SO_Characters _character;
    public Line[] lines;
    public int storyValue;
}

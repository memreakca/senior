using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest List", menuName = "Quest System/QuestList")]
public class QuestListObject : ScriptableObject
{
    public MainQuestsObject[] mainQuestObjects;
    
}

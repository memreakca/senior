using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : IInteractable
{
    public string[] dialogue;
    public string name;

    public void Interact()
    {
        Debug.Log("Interacting with NPC.");
    }
}

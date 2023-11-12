using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtons : MonoBehaviour
{
    public GameObject InvUI;
    public GameObject CraftUI;
    public void InvButtonClick()
    {
        InvUI.SetActive(true);
        CraftUI.SetActive(false);
    }

    public void CraftButtonClick()
    {
        InvUI.SetActive(false);
        CraftUI.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftManager : MonoBehaviour
{
    [SerializeField]
    private RecipeListObject recipeSO;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.O))
        {
            recipeSO.recipeList[0].Craft();
        }
    }
}

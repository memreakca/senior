using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DisplayCraftigScreen : MonoBehaviour
{
    [SerializeField] private RecipeListObject recipeSO;
    [SerializeField] private GameObject recipePrefab;
    [SerializeField] RectTransform recipeListContent;

    private void Start()
    {
        createRecipeTab();
    }
    private void createRecipeTab()
    {
        foreach (CraftRecipeObject item in recipeSO.recipeList)
        {
            var obj = Instantiate(recipePrefab, Vector3.zero, Quaternion.identity);
            obj.transform.SetParent(recipeListContent);
            obj.transform.GetComponentsInChildren<Image>()[1].sprite = item.uiDisplay;
            obj.transform.GetComponentInChildren<TextMeshProUGUI>().text = item.result.Name;
        }
    }
}
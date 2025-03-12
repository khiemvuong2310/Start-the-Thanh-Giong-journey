using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Shop : MonoBehaviour
{
    //private Transform shopItemTemplate;
    //private Transform container;

    //private void Awake()
    //{
    //    container = transform.Find("container");
    //    shopItemTemplate = transform.Find("shopItemTemplate");
    //    shopItemTemplate.gameObject.SetActive(false);
    //}
    //private void Start()
    //{
    //    CreateItemButton(Item.GetSprite(Item.ItemType.HealthPotion), "HealthPotion", Item.GetCost(Item.ItemType.HealthPotion), 0);
    //    CreateItemButton(Item.GetSprite(Item.ItemType.StaminaPotion), "StaminaPotion", Item.GetCost(Item.ItemType.StaminaPotion), 1);
    //}
    //private void CreateItemButton(Sprite itemSprite, string itemName, int itemCost, int positionIndex)
    //{
    //    Transform shopItemTransform = Instantiate(shopItemTemplate, container);
    //    ReactTransform shopItemRectTransform = shopItemTransform.GetComponet<shopItemRectTransform>();

    //    float shopItemHeight = 30f;
    //    shopItemRectTransform.anchoredPosition = new Vector2(0, -shopItemHeight * positionIndex);

    //    shopItemTransform.Find("itemName").GetComponent<TextMeshProGUI>().SetText(itemName);
    //    shopItemTransform.Find("costText").GetComponent<TextMeshProGUI>().SetText(itemCost.toString());

    //    shopItemTransform.Find("itemImage").GetComponent<Image>().sprite = itemSprite;
    //}
}

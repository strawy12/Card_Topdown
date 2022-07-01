using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoCardUI : MonoBehaviour
{
    private static CardInventoryManager _inventoryManager;

    protected static CardInventoryManager InventoryManager
    {
        get
        {
            if(_inventoryManager == null)
            {
                _inventoryManager = FindObjectOfType<CardInventoryManager>();
            }

            return _inventoryManager;
        }
    }
}

using UnityEngine;
using System.Collections.Generic;

public class CollectionManager : MonoBehaviour
{
    [System.Serializable]
    public class ItemSlot
    {
        public int itemID;              // ID of the item that goes in this slot
        public Transform slotTransform; // The position/rotation where the item should go
        public bool isFilled = false;
    }
    [Header("General assigners")]
    [SerializeField] private WorldLocation story;

    [Header("Puzzle details")]
    [SerializeField] private NPC_Alpaca floris;
    [SerializeField] private NPC_Wolf bobo;
    [SerializeField] private NPC_Duck kwek;
    [SerializeField] private string puzzleName = "";


    [Header("Item Slots")]
    public List<ItemSlot> itemSlots = new List<ItemSlot>();

    public void CheckAllSlotsFilled()
    {
        Debug.Log("Checking all slots:");
        foreach (var slot in itemSlots)
        {
            if (!slot.isFilled) 
            {
                Debug.Log("slot " +  slot.itemID + " is not filled.");
                return; // Not all slots are filled
            }
        }

        Debug.Log("All slots filled!");
        CompletedPuzzle();
    }

    private void CompletedPuzzle()
    {
        switch (puzzleName)
        {
            case "tennisballs":
                if (kwek != null) kwek.satisfyTask = true;
                break;
            case "groceries":
                if (floris != null) floris.satisfyTask = true;
                break;
            case "wolf":
                if (bobo != null) bobo.satisfyTask = true;
                break;
        }
    }
}
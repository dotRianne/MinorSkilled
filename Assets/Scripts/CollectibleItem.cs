using UnityEngine;
using TMPro;

[RequireComponent(typeof(Collider))]
public class CollectibleItem : MonoBehaviour
{
    public int itemID;
    public bool collected = false;
    public Vector3 placeScale;

    [Header("References")]
    public Transform holdSpot;   // Assigned manually in Inspector
    public TMP_Text TXT_input;

    private bool playerInRange = false;
    private bool atCollectionSite = false;

    // 👇 Track currently held item globally so only one can be held
    public static CollectibleItem currentlyHeldItem;
    public CollectionManager collectionManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (!collected && currentlyHeldItem == null) TXT_input.SetText("[Mouse1] Pick up item.");
        }

        if (other.CompareTag("CollectionSite"))
        {
            atCollectionSite = true;
            if (currentlyHeldItem == this) TXT_input.SetText("[Mouse1] Place the item.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            TXT_input.text = "";
        }

        if (other.CompareTag("CollectionSite"))
        {
            atCollectionSite = false;
            TXT_input.text = "";
        }
    }

    private void Update()
    {
        // Pick up logic
        if (playerInRange && !collected && currentlyHeldItem == null && Input.GetMouseButtonDown(0))
        {
            Pickup();
        }

        // Drop logic
        else if (currentlyHeldItem == this && Input.GetMouseButtonDown(0))
        {
            if (atCollectionSite) PlaceAtCollectionSite();
            else Drop();
        }
    }

    private void Pickup()
    {
        if (holdSpot == null)
        {
            Debug.LogError($"[{name}] HoldSpot not assigned!");
            return;
        }

        currentlyHeldItem = this;
        transform.SetParent(holdSpot, worldPositionStays: false);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

        TXT_input.text = "";

        Debug.Log($"[{name}] Picked up and parented to {holdSpot.name}");
    }

    public void Drop()
    {
        Debug.Log($"[{name}] Dropped");
        transform.SetParent(null);
        currentlyHeldItem = null;
        TXT_input.text = "";
    }

    private void PlaceAtCollectionSite()
    {
        if (collectionManager == null)
        {
            Debug.LogWarning($"[{name}] No CollectionManager assigned!");
            return;
        }

        // Find the slot that matches this item's ID
        CollectionManager.ItemSlot slot = collectionManager.itemSlots.Find(s => s.itemID == this.itemID);

        if (slot == null || slot.slotTransform == null)
        {
            Debug.LogWarning($"[{name}] No slot found for item ID {itemID}");
            return;
        }

        // Teleport to slot
        transform.SetParent(slot.slotTransform);
        transform.position = slot.slotTransform.position;
        transform.rotation = slot.slotTransform.rotation;
        transform.localScale = placeScale;
        collected = true;
        currentlyHeldItem = null;
        slot.isFilled = true;

        TXT_input.text = "";
        collectionManager.CheckAllSlotsFilled();

        //Debug.Log($"[{name}] placed in slot {slot.slotTransform.name}");
    }
}
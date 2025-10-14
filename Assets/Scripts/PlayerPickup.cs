using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public Transform holdPoint;

    private CollectibleItem heldItem;
    private CollectionManager collectionManager;
    private CollectionSite currentCollectionSite;

    public bool IsHoldingItem() => heldItem != null;

    public void HoldItem(CollectibleItem item)
    {
        heldItem = item;
    }

    private void Update()
    {
        // Drop or deliver
        if (heldItem != null && Input.GetMouseButtonDown(0))
        {
            if (currentCollectionSite != null && collectionManager != null)
            {
                // Deliver item
                // collectionManager.PlaceItemInSlot(heldItem);
                heldItem.collected = true;
                heldItem.transform.SetParent(null);
                heldItem = null;
            }
            else
            {
                // Drop item normally
                heldItem.Drop();
                heldItem = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        CollectionSite site = other.GetComponent<CollectionSite>();
        if (site != null)
        {
            currentCollectionSite = site;
            collectionManager = site.manager;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CollectionSite>() == currentCollectionSite)
        {
            currentCollectionSite = null;
            collectionManager = null;
        }
    }
}
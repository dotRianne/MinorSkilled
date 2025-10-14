using UnityEngine;

public class Collectible : MonoBehaviour
{
    public CollectiblesManager manager;
    public string type = "";
    public int amount = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            manager.Increase(type, amount);
            manager.Trigger(type);
            Destroy(this.gameObject);
        }
    }
}

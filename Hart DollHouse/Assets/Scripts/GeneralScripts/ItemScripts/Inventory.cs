using UnityEngine;

public class Inventory : MonoBehaviour {

    private Item[] items = new Item[8];

    #region Singleton
    public static Inventory instance;
    
    void Awake()
    {
        if (instance != null) {
            Debug.Log("WARNING! More than one instance of inventory is created.");
            return;
        }
        instance = this;
    }
    #endregion

    public bool AddToInvent(Item item) {
        if (item.isImportantItem) {
            this.items[item.id] = item;
            return true;
        }
        return false;
    }

    public bool Contains(int id) {
        return this.items[id] != null;
    }

    // May return null if object is not contained
    public Item RemoveFromInvent(int id) {
        Item item = this.items[id];
        this.items[id] = null;
        return item;
    }

    public Item GetItem(int id)
    {
        return this.items[id];
    }
}

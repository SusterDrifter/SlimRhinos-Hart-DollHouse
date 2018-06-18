using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Item", menuName = "Custom/Item")]
public class Item : ScriptableObject {

    public string itemName = "Default";
    public bool isImportantItem = false;
    public int id = -1;
    public Sprite icon;
    public List<string> description;
}

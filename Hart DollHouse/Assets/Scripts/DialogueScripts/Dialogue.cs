using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Custom/Dialogue")]
public class Dialogue : ScriptableObject{

    [TextArea(2, 4)]
    public string[] sentences;
}

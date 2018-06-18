using System.Collections;
using UnityEngine;

public class Drag : MonoBehaviour {
    Vector3 distance;
    float xCoor;
    float yCoor;

    private void OnMouseDown()
    {
        distance = Camera.main.WorldToScreenPoint(transform.position);
        xCoor = Input.mousePosition.x - distance.x;
        yCoor = Input.mousePosition.y - distance.y;
    }

    private void OnMouseDrag()
    {
        Vector3 curPosition = new Vector3(
            Input.mousePosition.x - xCoor,
            Input.mousePosition.y - yCoor,
            distance.z);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(curPosition);
        transform.position = worldPosition;
    }
}

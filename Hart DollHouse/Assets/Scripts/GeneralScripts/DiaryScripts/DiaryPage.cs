using UnityEngine;
using UnityEngine.UI;

public class DiaryPage : MonoBehaviour {

    public int pageNum;
    public int subPageNum;
    public string keyword = "KEYWORD";
    [SerializeField] private Sprite source;
    [SerializeField] Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
        image.sprite = source;
        image.enabled = false;
    }

    public void EnableImage()
    {
        image.enabled = true;
    }

    public void DisableImage()
    {
        image.enabled = false;
    }
}

using UnityEngine;

public class Chapter1_3 : MonoBehaviour {

    public static Chapter1_3 instance;

    public bool diaryTrig = false;

    private int tabCount = 0;

    private void Awake()
    {
        if (instance != null)
            return;

        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && Inventory.instance.Contains(4))
        {
            ++tabCount;
        }

        if (tabCount >= 2 && !diaryTrig)
        {
            Ending();
            diaryTrig = true;
        }
    }
    public void Ending()
    {
        GameManager.instance.FadeNextChapter();
    }
}

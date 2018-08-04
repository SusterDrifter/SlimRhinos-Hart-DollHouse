using System.Collections;
using UnityEngine;

[RequireComponent(typeof(UIFader), typeof(CanvasGroup))]
public class DiaryFlip : MonoBehaviour {

    [SerializeField] private DiaryPage[][] pages;
    [SerializeField] private UIFader fader;
    [SerializeField] private CanvasGroup UIElement;
    [SerializeField] private float fadeDuration = 0.1f;
    [SerializeField] public int lastPageIndex;

    public int diarySize = 10;
    public int subDiarySize = 2;

    void Start() {

        DiaryPage[] obj = GetComponentsInChildren<DiaryPage>();
        fader = GetComponent<UIFader>();
        UIElement = GetComponent<CanvasGroup>();
        pages = new DiaryPage[diarySize][];

        for (int i = 0; i < obj.Length; i++)
        {
            DiaryPage page = obj[i];

            if (pages[page.pageNum] == null)
              pages[page.pageNum] = new DiaryPage[subDiarySize];

            pages[page.pageNum][page.subPageNum] = page;
        }
            
        lastPageIndex = pages.Length;
        UIElement.alpha = 0;
    }

    public void BeginReading(int left = 0, int right = 1)
    {
        StopAllCoroutines();
        fader.FadeIn(UIElement, fadeDuration);
        OrganisePage(left);
        OrganisePage(right);
        // Fade in text
        
    }

    public void StopReading()
    {
        StopAllCoroutines();
        fader.FadeOut(UIElement, fadeDuration);
    }

    public void ChangePage(int prevPage, int curPage)
    {
        StopAllCoroutines();
        int testLeft = curPage;
        int testRight = curPage + 1;
        int newLeft = -1;
        int newRight = -1;

        if (testLeft >= 0 && testLeft < lastPageIndex && pages[testLeft] != null)
            newLeft = testLeft;
        if (testRight >= 0 && testRight < lastPageIndex && pages[testRight] != null)
            newRight = testRight;

        StartCoroutine(TransitionPage(prevPage, prevPage + 1, newLeft, newRight));
    }

    private IEnumerator TransitionPage(int oldLeft, int oldRight, int newLeft, int newRight)
    {
        // fade out
        yield return StartCoroutine(fader.FadeUI(UIElement, UIElement.alpha, 0, fadeDuration / 2));

        // Disable and Enable new image
        OrganisePage(oldLeft, newLeft);
        OrganisePage(oldRight, newRight);

        // fade in
        StartCoroutine(fader.FadeUI(UIElement, UIElement.alpha, 1, fadeDuration / 2));
    }

    private void OrganisePage(int newPage)
    {
        if (newPage != -1)
        {
            for (int i = 0; i < pages[newPage].Length; i++)
            {
                if(pages[newPage][i] != null)
                    pages[newPage][i].EnableImage();
            }
        }
    }

    private void OrganisePage(int oldPage, int newPage)
    {

        for (int i = 0; i < pages[oldPage].Length; i++)
        {
            if(pages[oldPage][i] != null)
                pages[oldPage][i].DisableImage();
        }
        OrganisePage(newPage);
    }

}

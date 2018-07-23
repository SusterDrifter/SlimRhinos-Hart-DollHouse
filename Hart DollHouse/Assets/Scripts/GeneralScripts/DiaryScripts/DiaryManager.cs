using UnityEngine;

public class DiaryManager : MonoBehaviour {

    [SerializeField] private DiaryFlip pages;
    [SerializeField] private int curPage = -2;
    [SerializeField] private int prevPage;
    
    public bool isActive;
    private CanvasGroup UIElement;
    [SerializeField] private CanvasGroup cover;
    [SerializeField] private CanvasGroup pageBg;
    [SerializeField] private CanvasGroup backCover;
    [SerializeField] private UIFader fader;

    private Sound diaryOpen;
    private Sound diaryClose;
    private Sound diaryActive;
    private Sound diaryDeactivate;

    private void Awake()
    {
        pages = GetComponentInChildren<DiaryFlip>();
        UIElement = GetComponent<CanvasGroup>();

        cover = GameObject.FindGameObjectWithTag("DiaryCover").GetComponent<CanvasGroup>();
        pageBg = GameObject.FindGameObjectWithTag("DiaryPages").GetComponent<CanvasGroup>();
        backCover = GameObject.FindGameObjectWithTag("DiaryBackCover").GetComponent<CanvasGroup>();
        fader = GetComponent<UIFader>();

        UIElement.alpha = 0;
        cover.alpha = 1;
        pageBg.alpha = 0;
        backCover.alpha = 0;
        prevPage = curPage;
    }

    private void Update()
    {
        if(isActive)
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0f) {
                curPage += 2;
                curPage = Mathf.Clamp(curPage, -2, pages.lastPageIndex);
            } else if (Input.GetAxis("Mouse ScrollWheel") > 0f) {
                curPage -= 2;
                curPage = Mathf.Clamp(curPage, -2, pages.lastPageIndex);
            }

            if (curPage != prevPage) {
                if (curPage == -2 && prevPage > curPage) // Going to cover
                {
                    StopCoroutine("UITransitionAsync");
                    fader.UITransitionAsync(pageBg, cover, 2.0f);

                    if (diaryClose == null)
                        diaryClose = AudioManager.instance.GetSound(Sound.SoundType.SoundEffect, "DiaryClose");

                    AudioManager.instance.PlayClip(diaryClose);
                    pages.StopReading();
                }
                else if (prevPage == -2) // Going from cover
                {
                    StopCoroutine("UITransitionAsync");
                    fader.UITransitionAsync(cover, pageBg, 2.0f);

                    if (diaryOpen == null)
                        diaryOpen = AudioManager.instance.GetSound(Sound.SoundType.SoundEffect, "DiaryOpen");

                    AudioManager.instance.PlayClip(diaryOpen);
                    pages.BeginReading();
                }
                else if (curPage == pages.lastPageIndex && prevPage < curPage) // Going to backcover
                {
                    StopCoroutine("UITransitionAsync");
                    fader.UITransitionAsync(pageBg, backCover, 2.0f);

                    if (diaryClose == null)
                        diaryClose = AudioManager.instance.GetSound(Sound.SoundType.SoundEffect, "DiaryClose");

                    AudioManager.instance.PlayClip(diaryClose);
                    pages.StopReading();
                }
                else if (prevPage == pages.lastPageIndex) // Going from backcover
                {
                    StopCoroutine("UITransitionAsync");
                    fader.UITransitionAsync(backCover, pageBg, 2.0f);

                    if (diaryOpen == null)
                        diaryOpen = AudioManager.instance.GetSound(Sound.SoundType.SoundEffect, "DiaryOpen");

                    AudioManager.instance.PlayClip(diaryOpen);
                    pages.BeginReading(pages.lastPageIndex - 2, pages.lastPageIndex - 1);
                }
                else
                {
                    pages.ChangePage(prevPage, curPage);
                }

                prevPage = curPage;
            }
        }   
    }

    public void ActivateDiary()
    {
        MainUIManager.instance.isUIActive = true;
        isActive = true;
        UIElement.alpha = 1;

        if (diaryActive == null)
            diaryActive = AudioManager.instance.GetSound(Sound.SoundType.SoundEffect, "DiaryActive");

        AudioManager.instance.PlayClip(diaryActive);
    }

    public void DeactivateDiary()
    {
        MainUIManager.instance.isUIActive = false;
        isActive = false;
        UIElement.alpha = 0;

        if (diaryDeactivate == null)
            diaryDeactivate = AudioManager.instance.GetSound(Sound.SoundType.SoundEffect, "DiaryDeactivate");

        AudioManager.instance.PlayClip(diaryDeactivate);
    }
}

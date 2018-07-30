using UnityEngine;
using System.Collections;

public class Chapter2_4 : MonoBehaviour {

    [SerializeField] private Dialogue diaryDiag;
    [SerializeField] private Dialogue bathroomSceneDiag;
    [SerializeField] private Dialogue bathroomSceneTwoDiag;
    [SerializeField] private Vector3 rotationToTrunk;
    [SerializeField] private Item Diary;

    private Animator animator;
    private bool diaryTrig = false;

    #region Singleton
    public static Chapter2_4 instance;
    
    private void Awake()
    {
        if (instance != null)
            return;

        instance = this;
        animator = GetComponent<Animator>();
    }
    #endregion

    void Update () {

        if (Input.GetKeyDown(KeyCode.Tab) && !diaryTrig)
        {
            TriggerDiaryDiag();
            diaryTrig = true;
        }
    }

    public void TriggerDiaryDiag()
    {
        MainUIManager.instance.GetDialogueUIManager().GetManager().BeginDialogue(diaryDiag);
    }

    public void TriggerBathDiag()
    {
        MainUIManager.instance.GetDialogueUIManager().GetManager().BeginDialogue(bathroomSceneDiag);
    }

    public void TriggerBathTwoDiag()
    {
        MainUIManager.instance.GetDialogueUIManager().GetManager().BeginDialogue(bathroomSceneTwoDiag);
    }

    private void ActivateDiaryScene()
    {
        MainUIManager.instance.GetDiary().ActivateDiary();
    }

    public void ActivateClamping()
    {
        ClampingTrigger.instance.ActivateLocking();
    }

    public void DeactivateClamping()
    {
        ClampingTrigger.instance.DeactivateLocking();
    }

    public void DeactivateTransition(GameObject obj, Sound sound)
    {
        StartCoroutine(CoroutineDestroyObj(obj, sound, 0.1f, 2f)); 
    }

    IEnumerator CoroutineDestroyObj(GameObject obj, Sound sound, float fadeDuration, float delayDur)
    {
        MainUIManager.instance.GetBlackScreen().BlackFadeIn(fadeDuration);

        if (sound.clip)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.spatialBlend = sound.spatialBlend;

            sound.source.loop = sound.loop;
            sound.source.playOnAwake = sound.playOnAwake;
        }

        yield return new WaitForSecondsRealtime(fadeDuration);

        if (sound.clip)
            sound.source.Play();

        if (obj)
            obj.gameObject.SetActive(false);

        MainUIManager.instance.GetBlackScreen().BlackFadeOutDelayBy(delayDur);
    }

    public void BathroomScene()
    {
        animator.SetTrigger("Bathroom");
    }

    public void CameraRotation()
    {
        Camera.main.transform.Rotate(rotationToTrunk);
        if (Diary)
            Inventory.instance.AddToInvent(Diary);
    }

}

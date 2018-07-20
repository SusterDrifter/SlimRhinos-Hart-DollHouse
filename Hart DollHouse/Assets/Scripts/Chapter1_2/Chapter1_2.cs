using UnityEngine;

public class Chapter1_2 : MonoBehaviour {

    [SerializeField] private Dialogue startDiag;
    [SerializeField] private Dialogue endingDiag;
    [SerializeField] private Dialogue ending2Diag;
    [SerializeField] private Sound scribble;

    void Start () {
        if (scribble.clip)
        {
            scribble.source = gameObject.AddComponent<AudioSource>();
            scribble.source.clip = scribble.clip;

            scribble.source.volume = scribble.volume;
            scribble.source.pitch = scribble.pitch;
            scribble.source.spatialBlend = scribble.spatialBlend;

            scribble.source.loop = scribble.loop;
            scribble.source.playOnAwake = scribble.playOnAwake;
        }
            
	}
	
    public void StartDiag()
    {
        MainUIManager.instance.GetDialogueUIManager().GetManager().BeginDialogue(startDiag);
    }

    public void EndDiag()
    {
        MainUIManager.instance.GetDialogueUIManager().GetManager().BeginDialogue(endingDiag);
    }

    public void EndTwoDiag()
    {
        MainUIManager.instance.GetDialogueUIManager().GetManager().BeginDialogue(ending2Diag);
    }

    public void ScribbleSound()
    {
        AudioManager.instance.PlayClip(scribble);
    }

	void Update () {
		
	}
}

using UnityEngine;

public class Chapter1_1 : MonoBehaviour {

    public bool hasRhymeGameFinished = false;
    public RhymeGame rhymeGame;

    #region Singleton
    public static Chapter1_1 instance;

    private void Awake()
    {
        if (instance != null)
            return;

        instance = this;
        rhymeGame = GetComponent<RhymeGame>();
    }
    #endregion
	
    public void RhymeGameFinish()
    {
        hasRhymeGameFinished = true;
        Debug.Log("You completed the rhyme!");
    }
}

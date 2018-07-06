using UnityEngine;

public class RhymeGame : MonoBehaviour {

    [HideInInspector] public bool rhymeOne = false;
    [HideInInspector] public bool rhymeTwo = false;
    [HideInInspector] public bool rhymeThree = false;
    [HideInInspector] public bool rhymeFour = false;

	public void Complete()
    {
        if (rhymeOne && rhymeTwo && rhymeThree && rhymeFour)
        {
            Chapter1_1.instance.RhymeGameFinish();
        }
    }
}

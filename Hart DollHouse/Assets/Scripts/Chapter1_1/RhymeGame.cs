using UnityEngine;

public class RhymeGame : MonoBehaviour {

    [HideInInspector] public bool rhymeOne = false, rhymeTwo = false, rhymeThree = false, rhymeFour = false;

    [SerializeField] private ArticleObject rhymeOneObj;
    [SerializeField] private ArticleObject rhymeTwoObj;
    [SerializeField] private ArticleObject rhymeThreeObj;
    [SerializeField] private ArticleObject rhymeFourObj;
    [SerializeField] private TrunkKey trunkKey;

    public void ActivateOtherDoll()
    {
        if (rhymeThreeObj) 
            rhymeThreeObj.enabled = true;
        if (rhymeFourObj)
            rhymeFourObj.enabled = true;
    }

    public void ActivateFirstDoll()
    {
        if (rhymeTwo && rhymeThree && rhymeFour)
        {
            AudioManager.instance.PlayClip(Sound.SoundType.SoundEffect, "SuddenEntry");
            if (rhymeOneObj)
                rhymeOneObj.gameObject.SetActive(true);
            if (trunkKey)
            {
                trunkKey.GetComponent<TrunkKey>().enabled = false;
                trunkKey.gameObject.SetActive(true);
            }
        }
    }

    public void ActivateTrunkKey()
    {
        if (trunkKey)
        {
            trunkKey.GetComponent<TrunkKey>().enabled = true;
        }
    }

    public void Complete()
    {
        if (rhymeOne && rhymeTwo && rhymeThree && rhymeFour)
        {
            Chapter1_1.instance.RhymeGameFinish();
        }
    }
}

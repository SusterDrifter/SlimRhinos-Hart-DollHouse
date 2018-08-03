using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsEscape : MonoBehaviour {

    private bool hasPlayedMusic = false;

	void Update () {

        if (!hasPlayedMusic)
        {
            AudioManager.instance.PlayClip(Sound.SoundType.BackgroundMusic, "Haunted");
            hasPlayedMusic = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
            SceneManager.LoadScene(0);
	}
}

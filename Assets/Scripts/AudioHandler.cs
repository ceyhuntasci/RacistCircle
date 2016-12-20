using UnityEngine;
using System.Collections;

public class AudioHandler : MonoBehaviour {

    public AudioSource pop1;
    public AudioSource pop2;
    public AudioSource pop3;
    public AudioSource illegalMove;
    public AudioSource UItap;
    public AudioSource victory;
    public AudioSource lose;
    public AudioSource music;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayUISound()
    {
        UItap.Play();
    }
}

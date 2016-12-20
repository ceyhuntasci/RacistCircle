using UnityEngine;
using System.Collections;
using DG.Tweening;

public class PlayButton : MonoBehaviour {

    private Animator animator;
    public Canvas firstCanvas;
   

	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	        
	}

    public void Click()
    {

        animator.Play("PlayButton");
        DOVirtual.DelayedCall( 0.4f , () => firstCanvas.enabled = false);
     
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class Gem : MonoBehaviour
{
    public GameLogic gameLogic;
    public string Color;
    public Rigidbody2D rb;
    public float speed;
    public bool isSpecial;

    public ParticleSystem trail;
    public GameObject EmptyBubble;
    public float trailRotation;
    private Animator animator;
    private Vector2 pausedVelocity;

    public RectTransform scoreText;

    public bool slowed;
    private float FloatScaleX;
    private float FloatScaleY;
    bool popped;
    bool freezed;
    void Start()
    {
        FloatScaleX = UnityEngine.Random.Range(0.4f , 0.5f);

        FloatScaleY = FloatScaleX > 0.45f ? UnityEngine.Random.Range(0.4f, 0.45f) : UnityEngine.Random.Range(0.45f, 0.5f);
        popped = false;
        freezed = false;
        gameLogic = GameObject.FindWithTag("GameController").GetComponent<GameLogic>();
        scoreText = GameObject.FindWithTag("ScoreText").GetComponent<RectTransform>();
        animator = GetComponent<Animator>();

        transform.DOScaleX(FloatScaleX, 0.5f).SetLoops(50, LoopType.Yoyo);
        transform.DOScaleY(FloatScaleY, 0.5f).SetLoops(50, LoopType.Yoyo);

    }


    void FixedUpdate()
    {

        if (Mathf.Abs(transform.position.x) > 6f || Mathf.Abs(transform.position.y) > 9.5f)
        {
            gameLogic.gemsInside.Remove(this);
            Destroy(this.gameObject);
        }

        if (gameLogic.slowMo && !slowed)
        {
            rb.velocity = new Vector2(rb.velocity.x / 3, rb.velocity.y / 3);

            trail.playbackSpeed = 1.62f / 3;
            slowed = true;
        }
        else if (!gameLogic.slowMo && slowed)
        {
            rb.velocity = new Vector2(rb.velocity.x * 3, rb.velocity.y * 3);
            trail.playbackSpeed = 1.62f;
            slowed = false;
        }


        if (gameLogic.gamestate == GameLogic.GameState.Ended)
        {
            if (!popped)
            {
                EndLevelPop();
            }

        }
        else if (gameLogic.gamestate == GameLogic.GameState.Paused)
        {
            if (!freezed)
            {
                freezed = true;
                pausedVelocity = rb.velocity;
                rb.velocity = new Vector2(0f, 0f);
            }

        }
        else if (gameLogic.gamestate == GameLogic.GameState.Ongoing)
        {
           
                 
        }

    }

    public void PopBubble(float delay, bool scoreGained)
    {
        int rotation = UnityEngine.Random.Range(0, 91);

        GetComponent<Collider2D>().enabled = false;
        rb.velocity = rb.velocity / 3;
        trail.enableEmission = false;
        string animationName = Color + "BubblePop";



        GameObject bubbleClone = null;
    
        DOVirtual.DelayedCall(delay, () => animator.Play(animationName));
        DOVirtual.DelayedCall(delay, () => gameLogic.PopSound());
        DOVirtual.DelayedCall(delay, () => transform.rotation = Quaternion.Euler(0, 0, rotation));

        if (scoreGained)
        {
            DOVirtual.DelayedCall(delay + 0.49f, () => bubbleClone = (GameObject)Instantiate(EmptyBubble, transform.position, transform.rotation));
            DOVirtual.DelayedCall(delay + 0.49f, () => bubbleClone.GetComponent<SpriteRenderer>().DOFade(0f, 0f));
            DOVirtual.DelayedCall(delay + 0.49f, () => bubbleClone.GetComponent<SpriteRenderer>().DOFade(0.75f, 0.3f));
            DOVirtual.DelayedCall(delay + 0.49f, () => bubbleClone.transform.DOScale(0.25f, 0f));
            DOVirtual.DelayedCall(delay + 0.49f, () => bubbleClone.transform.DOMove(scoreText.position, 0.5f));
        }

        DOVirtual.DelayedCall(delay + 1f, () => Destroy(bubbleClone));
        DOVirtual.DelayedCall(delay + 0.5f, () => this.gameObject.transform.DOKill());
        DOVirtual.DelayedCall(delay + 0.5f, () => Destroy(this.gameObject));

    }

    public void EndLevelPop()
    {
        popped = true;
        rb.velocity = rb.velocity / 3;
        trail.enableEmission = false;
        string animationName = Color + "BubblePop";
        DOVirtual.DelayedCall(0.5f, () => animator.Play(animationName));
        DOVirtual.DelayedCall(1f, () => Destroy(this.gameObject));
    }

    public void UnFreeze()
    {
        rb.velocity = pausedVelocity;
    }

    public void PlayGlowAnim()
    {
        string animationName = Color + "BubbleGlow";
        if(isSpecial){
            animationName = Color + "BubbleSpecialGlow";
        }
        animator.Play(animationName);
    }

    public void PlayIdleAnim()
    {
        string animationName = Color + "BubbleIdle";
        if (isSpecial)
        {
            animationName = Color + "BubbleSpecial";
        }
        animator.Play(animationName);
    }

    public void PlaySpecialAnim()
    {
        string animationName = Color + "BubbleSpecial";
        Debug.Log(animationName);
        DOVirtual.DelayedCall(0f, () => animator.Play(animationName));
    }
}

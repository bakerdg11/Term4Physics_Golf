using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Add this to use TextMesh Pro

public class PlayerController : MonoBehaviour
{
    public GameObject golfBall;

    public Animator anim;

    public Sound soundManager;

    public float countdownDuration = 3f;
    private float currentTime;
    private float finalTime;
    private bool isCountingDown = false;

    public TextMeshProUGUI timerText;

    private Coroutine countdownCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        if (golfBall == null)
        {
            golfBall = GameObject.FindWithTag("GolfBall");
        }

        currentTime = countdownDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && !isCountingDown)
        {
            isCountingDown = true;
            currentTime = countdownDuration;
            countdownCoroutine = StartCoroutine(StartCountdown());
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopCountdown();
            anim.SetTrigger("JamesDrive");
        }

        if (isCountingDown && timerText != null)
        {
            timerText.text = currentTime.ToString("F3");
        }
    }

    private IEnumerator StartCountdown()
    {
        while (currentTime > -1)
        {
            currentTime -= Time.deltaTime;
            currentTime = Mathf.Max(currentTime, -1);
            yield return null;
        }

        isCountingDown = false;
    }

    private void StopCountdown()
    {
        if (isCountingDown && countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
            isCountingDown = false;
            finalTime = currentTime;
            Debug.Log("Countdown stopped, finalTime: " + finalTime);
        }
    }

    public void ResetToJamesSetup()
    {
        anim.ResetTrigger("JamesDrive");
        anim.Play("JamesSetup");
    }


    public void TriggerBallMovement() // This is the Animation Event
    {
        soundManager.PlayDriveSound();

        GolfBall golfBallPhysics = golfBall.GetComponent<GolfBall>();

        // Sets InitialVelocity in GolfBall

        if (finalTime > -0.3f && finalTime <= -0.2f)
        {
            golfBallPhysics.initialVelocity = new Vector3(
                Random.Range(10f, 12f),
                Random.Range(15f, 17f),
                Random.Range(-4f, 4f));
        }
        else if (finalTime > -0.2f && finalTime <= -0.1f)
        {
            golfBallPhysics.initialVelocity = new Vector3(
                Random.Range(11f, 13f),
                Random.Range(16f, 18f),
                Random.Range(-3f, 3f));
        }
        else if (finalTime > -0.1f && finalTime <= -0.02f)
        {
            golfBallPhysics.initialVelocity = new Vector3(
                Random.Range(12f, 14f),
                Random.Range(17f, 19f),
                Random.Range(-2f, 2f));
        }
        else if (finalTime > -0.02f && finalTime <= 0.02f)
        {
            golfBallPhysics.initialVelocity = new Vector3(14f, 19, 0f);
        }
        else if (finalTime > 0.02f && finalTime <= 0.1f)
        {
            golfBallPhysics.initialVelocity = new Vector3(
                Random.Range(12f, 14f),
                Random.Range(17f, 19f),
                Random.Range(-2f, 2f));
        }
        else if (finalTime > 0.1f && finalTime <= 0.2f)
        {
            golfBallPhysics.initialVelocity = new Vector3(
                Random.Range(11f, 13f),
                Random.Range(16f, 18f),
                Random.Range(-3f, 3f));
        }
        else if (finalTime > 0.2f && finalTime <= 0.3f)
        {
            golfBallPhysics.initialVelocity = new Vector3(
                Random.Range(10f, 12f),
                Random.Range(15f, 17f),
                Random.Range(-4f, 4f));
        }
        else if (finalTime > 0.3f && finalTime <= 0.4f)
        {
            golfBallPhysics.initialVelocity = new Vector3(
                Random.Range(9f, 11f),
                Random.Range(14f, 16f),
                Random.Range(-5f, 5f));
        }
        else if (finalTime > 0.4f && finalTime <= 0.5f)
        {
            golfBallPhysics.initialVelocity = new Vector3(
                Random.Range(8f, 10f),
                Random.Range(13f, 15f),
                Random.Range(-6f, 6f));
        }
        else if (finalTime > 0.5f && finalTime <= 0.6f)
        {
            golfBallPhysics.initialVelocity = new Vector3(
                Random.Range(7f, 9f),
                Random.Range(12f, 14f),
                Random.Range(-7f, 7f));
        }
        else if (finalTime > 0.6f)
        {
            soundManager.PlayDuffedShot();
            golfBallPhysics.initialVelocity = new Vector3(
                Random.Range(3f, 6f),
                Random.Range(2f, 4f),
                Random.Range(-8f, 8f));
        }

        Debug.Log("Current time: " + finalTime + " | Velocity: " + golfBallPhysics.initialVelocity);

        // Sets BallMovement in GolfBall
        golfBallPhysics.BallMovement();
    }
}
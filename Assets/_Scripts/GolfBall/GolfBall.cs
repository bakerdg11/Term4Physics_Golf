using TMPro;
using UnityEngine;

public class GolfBall : MonoBehaviour
{
    Rigidbody rb;
    public Sound soundManager;

    // Physics components
    public Vector3 initialVelocity; //Velocity set by timer stopped
    public float gravity = -9.8f;   //Gravity
    public float drag = 0.1f;       //Air resistence
    float groundLevel = 15f;        //Required as physics is handled through code, not rigidbody
    private bool isMoving = false;  //Used to apply physics when = true
    private Vector3 velocity;        //Used to apply position based on gravity and drag
    private Vector3 position;        //Used to update the balls position
    private Vector3 initialPosition; //Used to calculate distances for records

    // Wind
    public bool inWindZone = false;
    public GameObject windZone;

    // Distance finders
    public GameObject closestToFlagFinder;
    public GameObject longestDriveFinder;
    public TextMeshProUGUI closestToFlagText;
    public TextMeshProUGUI longestDriveText;
    private float closestToFlagRecord = Mathf.Infinity;
    private float longestDriveRecord = 0f;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        position = transform.position;

        initialPosition = position;
    }

    void Update()
    {
        if (isMoving)
        {
            ApplyPhysics();
        }
        else
        {
            CalculateDistanceToFlag();
            CalculateLongestDrive();
        }
    }

    public void BallMovement()
    {
        // Set the initial velocity based on the club impact (this would be passed from your player controller)
        velocity = initialVelocity;
        isMoving = true;
    }

    void ApplyPhysics()
    {
        // Apply gravity to the Y-axis
        velocity.y += gravity * Time.deltaTime;

        // Drag slows the ball down gradually
        velocity *= (1 - drag * Time.deltaTime);

        // Update the ball's position
        position += velocity * Time.deltaTime;

        // Set the ball's new position
        transform.position = position;

        // Check if the ball hits the ground / No RB
        if (position.y <= groundLevel)
        {
            position.y = groundLevel;
            velocity.y = -velocity.y * 0.5f; //Ball Bounces
            velocity.x *= 0.5f;  //Reduces X Velocity
            velocity.z *= 0.5f;  //Reduces Y Velocity
        }

        // Stops ball when it gets slow enough
        if (velocity.magnitude < 0.2f)
        {
            velocity = Vector3.zero;
            isMoving = false;
        }

        // Applies wind
        if (inWindZone && windZone != null)
        {
            Vector3 windForce = windZone.GetComponent<WindArea>().direction * windZone.GetComponent<WindArea>().strength;
            velocity += windForce * Time.deltaTime;  // Add wind force to the velocity
        }
    }

    public void ResetShot()
    {
        transform.position = initialPosition;
        position = initialPosition;

        velocity = Vector3.zero;
        isMoving = false;
    }

    void CalculateDistanceToFlag()
    {
        if (closestToFlagFinder != null)
        {
            float distance = Vector3.Distance(transform.position, closestToFlagFinder.transform.position);
            distance = distance * 5;
            string distanceToFlag = distance.ToString("F2");

            // Applies record if it is broken
            if (distance < closestToFlagRecord)
            {
                closestToFlagRecord = distance;  // Updates Record
                closestToFlagText.text = "Distance To The Pin: " + distanceToFlag + " yds";
            }
        }
    }

    void CalculateLongestDrive()
    {
        if (longestDriveFinder != null)
        {
            float driveDistance = Vector3.Distance(transform.position, longestDriveFinder.transform.position);
            driveDistance = driveDistance * 5;
            string distanceFromDrive = driveDistance.ToString("F2");

            // Applies record if it is broken
            if (driveDistance > longestDriveRecord)
            {
                longestDriveRecord = driveDistance;  // Updates Record
                longestDriveText.text = "Longest Drive: " + distanceFromDrive + " yds";
                soundManager.PlayHighScoreMusic();
            }
        }
    }


    // Wind Triggers
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "WindArea")
        {

            inWindZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "WindArea")
        {
            inWindZone = false;
        }
    }
}
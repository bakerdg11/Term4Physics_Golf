using UnityEngine;
using UnityEngine.UI;

public class IngameMenu : BaseMenu
{
    public Button shootAgainButton;  // Reference to the Shoot Again button
    private GolfBall golfBall;
    private PlayerController playerController;

    public override void InitState(MenuController ctx)
    {
        base.InitState(ctx);
        state = MenuController.MenuStates.Ingame;

        // Find the GolfBall object and assign it (assuming it's tagged as "GolfBall")
        golfBall = GameObject.FindGameObjectWithTag("GolfBall").GetComponent<GolfBall>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        // Add listener to the Shoot Again button
        shootAgainButton.onClick.AddListener(ShootAgain);
    }

    // Method to reset the game for the next shot
    private void ShootAgain()
    {
        if (golfBall != null)
        {
            Debug.Log("Shooting again...");
            golfBall.ResetShot();
        }
        else
        {
            Debug.LogError("GolfBall not found!");
        }

        if (playerController != null)
        {
            playerController.ResetToJamesSetup(); // Reset the player's animation
        }
        else
        {
            Debug.LogError("PlayerController not found!");
        }
    }
}
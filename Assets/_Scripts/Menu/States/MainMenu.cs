using UnityEngine;
using UnityEngine.UI;

public class MainMenu : BaseMenu
{
    public Button playButton;
    public Button optionsButton;
    public Button quitButton;
    public Sound soundManager;

    public override void InitState(MenuController ctx)
    {
        base.InitState(ctx);
        state = MenuController.MenuStates.MainMenu;
        playButton.onClick.AddListener(() => 
        {
            context.SetActiveState(MenuController.MenuStates.Ingame);
            soundManager.GameMusic();
        });
        optionsButton.onClick.AddListener(() => context.SetActiveState(MenuController.MenuStates.Options));
        quitButton.onClick.AddListener(QuitGame);
    }

    public override void EnterState()
    {
        base.EnterState();
        Time.timeScale = 0.0f;
    }

    public override void ExitState()
    {
        Time.timeScale = 1.0f;
        base.ExitState();

    }

    private void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();

        // If running in the editor (for testing purposes)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

}

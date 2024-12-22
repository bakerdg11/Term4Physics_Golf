using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : Singleton<MenuController>
{
    public BaseMenu[] allMenus;

    public enum MenuStates
    {
        MainMenu, Options, Ingame
    }

    private BaseMenu currentState;
    private Dictionary<MenuStates, BaseMenu> menuDictionary = new Dictionary<MenuStates, BaseMenu>();
    private Stack<MenuStates> menuStack = new Stack<MenuStates>();

    // Start is called before the first frame update
    void Start()
    {
        if (allMenus == null) return;

        foreach (BaseMenu menu in allMenus)
        {
            if (menu == null) continue;

            Debug.Log($"Initializing menu: {menu.GetType().Name}");
            menu.InitState(this);

            if (!menuDictionary.ContainsKey(menu.state))
            {
                menuDictionary.Add(menu.state, menu);
            }
        }

        foreach (MenuStates state in menuDictionary.Keys)
        {
            Debug.Log($"Deactivating menu: {state}");
            menuDictionary[state].gameObject.SetActive(false);
        }

        Debug.Log("Setting active state to MainMenu");
        SetActiveState(MenuStates.MainMenu); // Ensure this line is after menus are added

    }

    public void SetActiveState(MenuStates newState, bool isJumpingBack = false)
    {
        //this menu state doesn't exist in the scene
        if (!menuDictionary.ContainsKey(newState)) return;

        if (currentState != null)
        {
            currentState.ExitState();
            currentState.gameObject.SetActive(false);
        }

        currentState = menuDictionary[newState];
        currentState.gameObject.SetActive(true);
        currentState.EnterState();

        if (!isJumpingBack)
        {
            menuStack.Push(newState);
        }
    }

    public void JumpBack()
    {
        if (menuStack.Count <= 1)
        {
            SetActiveState(MenuStates.MainMenu);
        }
        else
        {
            menuStack.Pop();
            SetActiveState(menuStack.Peek(), true);
        }
    }
}

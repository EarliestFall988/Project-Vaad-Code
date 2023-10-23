using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The menu controller is responsible for opening and closing the menu, and handling world objects that need to be disabled when the menu is open
/// </summary>
public class MenuController : MonoBehaviour
{
    /// <summary>
    /// The main instance of the menu controller
    /// </summary>
    public static MenuController main;

    [SerializeField]
    private GameObject _menu;
    [SerializeField]
    private bool _menuOpen = false;
    [SerializeField]
    private List<GameObject> _playerObjects = new List<GameObject>();

    public delegate void MenuChanged(bool menuOpen, MenuTabs tab);
    public static event MenuChanged OnMenuChanged;

    /// <summary>
    /// is the menu open?
    /// </summary>
    public bool IsMenuOpen => _menuOpen;

    void Awake()
    {
        main = this;
    }

    /// <summary>
    /// Add player game object to list of objects to disable when menu is open
    /// </summary>
    /// <param name="obj">the game object</param>
    public void AddPlayerGameObject(GameObject obj)
    {
        _playerObjects.Add(obj);
    }

    /// <summary>
    /// Remove player game object from list of objects to disable when menu is open
    /// </summary>
    /// <param name="obj">the game object</param>
    public void RemovePlayerGameObject(GameObject obj)
    {
        _playerObjects.Remove(obj);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }

        if (Input.GetKeyDown(KeyCode.I) && !_menuOpen)
        {
            OpenMenu(MenuTabs.Equipment);
        }
    }

    /// <summary>
    /// Toggle the menu open or closed
    /// </summary>
    public void ToggleMenu()
    {
        if (_menuOpen)
        {
            CloseMenu();
        }
        else
        {
            OpenMenu(MenuTabs.Equipment);
        }
    }

    /// <summary>
    /// Open the menu
    /// </summary>
    /// <param name="tabs">
    /// the tab to open the menu to
    /// </param>
    public void OpenMenu(MenuTabs tabs)
    {

        if (!GameManager.main.CanOpenMenu)
            return;

        _menuOpen = true;
        OnMenuChanged?.Invoke(_menuOpen, tabs);

        for (int i = 0; i < _playerObjects.Count; i++)
        {
            if (_playerObjects[i] != null)
            {
                _playerObjects[i].SetActive(false);
            }
        }

        _menu.SetActive(true);
    }

    /// <summary>
    /// Close the menu
    /// </summary>
    public void CloseMenu()
    {

        _menuOpen = false;
        OnMenuChanged?.Invoke(_menuOpen, MenuTabs.None);

        for (int i = 0; i < _playerObjects.Count; i++)
        {
            if (_playerObjects[i] != null)
            {
                _playerObjects[i].SetActive(true);
            }
        }

        _menu.SetActive(false);
    }
}

/// <summary>
/// The tabs in the menu to switch to
/// </summary>
public enum MenuTabs
{
    None,
    Game,
    Equipment,
    Settings,
    Credits
}

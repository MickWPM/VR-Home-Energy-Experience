using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class LeftControllerMenu : MonoBehaviour
{

    [SerializeField] private InputActionReference nextMenuActionReference;
    public enum ControllerMenu { FloorplanNav, RewiringConnection}
    public ControllerMenu activeMenu;
    public GameObject floorplanUI, reiwringUI;
    
    private int numMenus;
    private GameObject currentMenuGO;

    private void Awake()
    {
        numMenus = Enum.GetValues(typeof(ControllerMenu)).Length;
        currentMenuGO = floorplanUI;
        floorplanUI.SetActive(true);
        reiwringUI.SetActive(false);

        nextMenuActionReference.action.Enable();
    }

    private void SetActiveMenu(ControllerMenu newMenu)
    {
        if (activeMenu == newMenu) return;

        if (currentMenuGO != null) currentMenuGO.SetActive(false);
        switch (newMenu)
        {
            case ControllerMenu.FloorplanNav:
                currentMenuGO = floorplanUI;
                break;
            case ControllerMenu.RewiringConnection:
                currentMenuGO = reiwringUI;
                break;
            default:
                Debug.LogError($"Unhandled menu option: {newMenu}");
                break;
        }
        activeMenu = newMenu;
        currentMenuGO.SetActive(true);
    }

 
    
    [ContextMenu("Next menu")]
    public void NextMenu()
    {
        ControllerMenu newMenu = (ControllerMenu)((int)(activeMenu + 1) % numMenus);
        SetActiveMenu(newMenu);
    }
    private void NextMenuAction(InputAction.CallbackContext context)
    {
        NextMenu();
    }


    void OnEnable()
    {
        nextMenuActionReference.action.Enable();
        nextMenuActionReference.action.performed += NextMenuAction;
    }

    void OnDisable()
    {
        nextMenuActionReference.action.performed -= NextMenuAction;
        nextMenuActionReference.action.Disable();
    }
}
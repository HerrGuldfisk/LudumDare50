using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Basics.UI
{
    public class MenuController : MonoBehaviour
    {
        private List<Menu> menus = new List<Menu>();

        private List<Menu> menuTree = new List<Menu>();

        [ReadOnly]
        [SerializeField] private Menu currentlyActiveMenu = null;

        [Tooltip("If the MenuController is a main menu")]
        [SerializeField] private bool isMainMenu;

        void Start()
        {
            menus = GetAllSubMenus();

            foreach(Menu menu in menus)
            {
                menu.gameObject.SetActive(false);
            }

            if (isMainMenu)
            {
                OpenMenu(0);
            }
        }

        /// <summary>
        /// Returns all child objects with Menu component.
        /// </summary>
        /// <returns></returns>
        private List<Menu> GetAllSubMenus()
        {
            List<Menu> menuList = new List<Menu>();
            foreach (Transform child in transform)
            {
                if (child.gameObject.GetComponent<Menu>() != null)
                {
                    menuList.Add(child.GetComponent<Menu>());
                }
            }

            return menuList;
        }


        public void OpenMenu(int menuIndex)
        {
            // Passes the Menu object to the other OpenMenu method.
            if (menuIndex < menus.Count && menuIndex >= 0)
            {
                OpenMenu(menus[menuIndex]);
            }
            else
            {
                Debug.LogWarning($"There is no menu with index: {menuIndex}");
            }
        }

        public void OpenMenu(Menu menu)
        {
            if (menus.Contains(menu))
            {
                if(menuTree.Count > 0)
                {
                    currentlyActiveMenu.gameObject.SetActive(false);
                }
                
                currentlyActiveMenu = menu;
                menuTree.Add(currentlyActiveMenu);
                currentlyActiveMenu.gameObject.SetActive(true);
            }
        }

        public void CloseMenu()
        {
            currentlyActiveMenu.gameObject.SetActive(false);
            menuTree.RemoveAt(menuTree.Count - 1);

            if(menuTree.Count != 0)
            {
                currentlyActiveMenu = menuTree[menuTree.Count - 1];
                currentlyActiveMenu.gameObject.SetActive(true);
            }
        }

        public void OnPause(InputValue value)
        {
            if(menuTree.Count == 0)
            {
                OpenMenu(0);
            }
            else if(menuTree.Count == 1 && isMainMenu)
            {
                // Do nothing
            }
            else
            {
                CloseMenu();
            }
        }
    }
}


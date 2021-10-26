using System.Collections.Generic;
using UnityEngine;

namespace TGM.FutureRacingGP.Managers
{
    public abstract class Menu : MonoBehaviour
    {
        [SerializeField] protected GameObject initialMenu;
        protected Stack<GameObject> subMenus = new Stack<GameObject>();


        // private void Update() => OnButtonOrKey_ToggleOrReturnMenu();


        public void OnButtonOrKey_ToggleOrReturnMenu()
        {
            // if (PlayerInputRef.PlayerInput.actions["CancelReturn"].triggered || isUIButton)
            {
                if (subMenus.Count > 0)
                {
                    RemoveSubMenu();

                    if (subMenus.Count <= 0)
                        initialMenu?.SetActive(true);

                    return;
                }

                if (subMenus.Count <= 0)
                    initialMenu?.SetActive(true);
            }
        }


        public void OnClick_AddSubMenu(GameObject subMenu)
        {
            initialMenu?.SetActive(false);

            if (subMenus.Count > 0)
                subMenus.Peek().SetActive(false);

            subMenus.Push(subMenu);
            subMenus.Peek().SetActive(true);
        }


        protected void RemoveSubMenu()
        {
            subMenus.Peek().SetActive(false);
            subMenus.Pop();

            if (subMenus.Count > 0)
                subMenus.Peek().SetActive(true);
        }
    }
}
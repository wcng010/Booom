using C_Script.Common.Model.EventCentre;
using C_Script.LevelSpecial.level_Start;
using C_Script.Manager;
using UnityEngine;
using UnityEngine.Serialization;


namespace C_Script.UI.Manager
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject endMenu;
        [SerializeField] private GameObject restartMenu;
        [SerializeField] private GameObject Instructions;
        private void Awake()
        {
            ScenesEventCentreManager.Instance.Subscribe(ScenesEventType.GameOver,OpenExitMenu);
            ScenesEventCentreManager.Instance.Subscribe(ScenesEventType.ReStart,OpenReStartMenu);
            InputManager.Instance.KeyEventEsc.AddListener(OpenReStartMenu);
            InputManager.Instance.KeyEventP.AddListener(OpenInstruction);
        }
        private void OpenExitMenu() => endMenu.SetActive(true);
        private void OpenReStartMenu()
        {
            if (!restartMenu.activeSelf) InputManager.Instance.CloseInput();
           else InputManager.Instance.OpenInput(); restartMenu.SetActive(!restartMenu.activeSelf);
        }

        private void OpenInstruction()
        {
            Instructions.SetActive(true);InputManager.Instance.CloseInput();
        }
    }
}

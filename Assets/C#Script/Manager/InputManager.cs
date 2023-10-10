using System;
using System.Collections.Generic;
using C_Script.Common.Model.Singleton;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace C_Script.Manager
{
    public class InputManager : NormSingleton<InputManager>
    {
        private PlayerInput PlayerInput => _playerInput? _playerInput : _playerInput = GetComponent<PlayerInput>();
        private PlayerInput _playerInput;
        public int InputX { get; private set; }
        public bool InputS { get; private set; }
        public int InputY { get; private set; }
        public bool InputSpace { get; private set; }
        public bool InputQ { get; private set; }
        public bool InputJ { get; set; }
        public bool InputK { get; private set; }
        public bool Alpha1 { get; private set; }
        public bool Alpha2 { get; private set; }
        public bool InputE { get; private set; }
        public bool InputEsc { get; private set; }

        public bool InputP { get; private set; }

        #region Event

        [NonSerialized]public UnityEvent KeyEventQ = new ();

        [NonSerialized]public UnityEvent KeyEventAlpha1 = new ();

        [NonSerialized] public UnityEvent KeyEventAlpha2 = new();

        [NonSerialized]public UnityEvent KeyEventE = new();

        [NonSerialized] public UnityEvent KeyEventEsc = new();

        [NonSerialized] public UnityEvent KeyEventP = new();
        #endregion
        
        public void CloseInput() => PlayerInput.enabled = false;
        public void OpenInput() => PlayerInput.enabled = true;
        public void OnMoveInput(InputAction.CallbackContext context)
        {
            var rawMovementInput = context.ReadValue<Vector2>();
            InputX = Mathf.RoundToInt(rawMovementInput.x);
            InputY = Mathf.RoundToInt(rawMovementInput.y);
        }

        public void OnInput_S(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                InputS = true;
            } 
            if (context.canceled)
            {
                InputS = false;
            }
        }

        public void OnInput_Space(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                InputSpace = true;
            }

            if (context.canceled)
            {
                InputSpace = false;
            }
        }
        
        public void OnInput_Q(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                InputQ = true;
                KeyEventQ?.Invoke();
            } 
            if (context.canceled)
            {
                InputQ = false;
            }
        }
        public void OnInput_J(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                InputJ = true;
            } 
            if (context.canceled)
            {
                InputJ = false;
            }
        }
        
        public void OnInput_K(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                InputK = true;
            } 
            if (context.canceled)
            {
                InputK = false;
            }
        }

        public void OnInput_Alpha1(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Alpha1 = true;
                KeyEventAlpha1?.Invoke();
            }
            if (context.canceled)
            {
                Alpha1 = false;
            }
        }
        
        public void OnInput_Alpha2(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Alpha2 = true;
                KeyEventAlpha2?.Invoke();
            }
            if (context.canceled)
            {
                Alpha2 = false;
            }
        }
        
        
        public void OnInput_E(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                InputE = true;
                KeyEventE?.Invoke();
            }
            if (context.canceled)
            {
                InputE = false;
            }
        }
        
        public void OnInput_Esc(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                InputEsc = true;
                KeyEventEsc?.Invoke();
            }
            if (context.canceled)
            {
                InputEsc = false;
            }
        }

        public void OnInput_P(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                InputP = true;
                KeyEventP?.Invoke();
            }
            if (context.canceled)
            {
                InputP = false;
            }
        }


    }
}
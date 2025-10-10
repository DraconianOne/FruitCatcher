using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.States
{
    public class CatchState : PlayerState
    {
        private InputAction _interactAction;
        void Start()
        {
            Debug.Log("CatchState::Start");
            _type = StateEnum.Catch;
            _interactAction = InputSystem.actions.FindAction("Interact");
        }
        
        void Update()
        {
            if (_interactAction.WasReleasedThisFrame())
            {
                _player.ChangeState(StateEnum.Default);
            }
        }

        public override void OnStateEnter()
        {
            Debug.Log("CatchState::OnStateEnter");
            crate.SetActive(true);
        }
        public override void OnStateExit()
        {
            crate.SetActive(false);
        }
        
    }
}

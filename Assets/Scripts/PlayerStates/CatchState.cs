using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.States
{
    public class CatchState : PlayerState
    {
        private InputAction _interactAction;
        
        protected override void Awake()
        {
            base.Awake();               
            _type = StateEnum.Catch;
        }

        void Start()
        {
            Debug.Log("CatchState::Start");
            
            _interactAction = InputSystem.actions.FindAction("Interact");
        }
        
        void Update()
        {
            if (_interactAction.WasReleasedThisFrame())
            {
                Debug.Log("CatchState:: Key Released");
                _player.ChangeState(StateEnum.Default);
            }
        }

        public override void OnStateEnter()
        {
            Debug.Log("CatchState::OnStateEnter");
            _player.crate.SetActive(true);
        }
        public override void OnStateExit()
        {
            _player.crate.SetActive(false);
        }
        
    }
}

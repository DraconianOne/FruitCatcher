using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.States
{
    public class DefaultState : PlayerState
    {
       private InputAction _moveAction;
        private InputAction _interactAction;

        protected override void Awake()
        {
            base.Awake();
            _type = StateEnum.Default;
        }
        
        public void Start()
        {
            Debug.Log("DefaultState::Start");
           
            _moveAction = InputSystem.actions.FindAction("Move");
            _interactAction = InputSystem.actions.FindAction("Interact");
        }
        // Update is called once per frame
        private void Update()
        {
            if (_moveAction.WasPressedThisFrame())
            {
                var dir = _moveAction.ReadValue<Vector2>();
                _player.transform.position += new Vector3( dir.x, dir.y, 0 );
            }

            if (_interactAction.WasPressedThisFrame())
            {
                Debug.Log("Interact key pressed");
                _player.ChangeState(StateEnum.Catch);
            }
        }

        public override void OnStateEnter()
        {
            _player.crate.SetActive(false);
        }
    }
}

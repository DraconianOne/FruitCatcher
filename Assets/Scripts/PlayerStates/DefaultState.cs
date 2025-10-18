using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.States
{
    public class DefaultState : PlayerState
    {
        private InputAction _moveAction;
        private InputAction _interactAction;
        
        private int _platformIndex = 0;
        [SerializeField] private float[] platformYPos = { -3.75f, -0.5f, 2.5f }; 

        protected override void Awake()
        {
            base.Awake();
            _type = StateEnum.Default;
        }
        
        public void Start()
        {
            _moveAction = InputSystem.actions.FindAction("Move");
            _interactAction = InputSystem.actions.FindAction("Interact");
        }

        public override void DoUpdate()
        {
            if (_moveAction.WasPressedThisFrame())
            {
                bool doMove = true;
                var dir = _moveAction.ReadValue<Vector2>();
                switch (dir.x, dir.y)
                {
                    case (0,1):
                        _platformIndex = Mathf.Min(_platformIndex+1, 2);
                        break;
                    case (0,-1):
                        _platformIndex = Mathf.Max(_platformIndex-1, 0);
                        break;
                    default: doMove = false;
                        break;
                }
                if (doMove)
                {
                    _player.transform.position = new Vector3(_player.DefaultXPos,
                        platformYPos[_platformIndex], 0);
                }
            }

            if (_interactAction.WasPressedThisFrame())
            {
                _player.ChangeState(StateEnum.Catch);
            }
        }
        
        public override void OnStateEnter()
        {
            _player.crate.SetActive(false);
        }
        
        public override void OnStateExit(){ }
        
    }
}

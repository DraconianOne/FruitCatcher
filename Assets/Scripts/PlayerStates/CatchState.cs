using UnityEngine;

namespace Player.States
{
    public class CatchState : PlayerState
    {
      
        [SerializeField] private float catchDelay = 0.5f;
        private float _catchWait = 0f;
        
        
        protected override void Awake()
        {
            base.Awake();               
            _type = StateEnum.Catch;
        }
        
        public override void DoUpdate()
        {
            if (_catchWait > 0f)
            {
                _catchWait -= Time.deltaTime;
            }
            else
            {
                _player.ChangeState(StateEnum.Default);
            }
        }

        public override void OnStateEnter()
        {
            _player.crate.SetActive(true);
            _player.ChangeSprite(_player.catchSprite);
            _catchWait = catchDelay;
        }
        public override void OnStateExit()
        {
            _player.crate.SetActive(false);
            _catchWait = 0f;
        }
        
    }
}

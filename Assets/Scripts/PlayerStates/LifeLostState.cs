using UnityEngine;

namespace Player.States
{
    public class LifeLostState : PlayerState
    {
        protected override void Awake()
        {
            base.Awake();               
            _type = StateEnum.LifeLost;
        }

        public override void DoUpdate() { }
        public override void OnStateEnter()
        {
            _player.transform.position = new Vector3(0f, _player.DefaultYPos, 0f);
        }

        public override void OnStateExit()
        {
            _player.transform.position = new Vector3(_player.DefaultXPos, _player.DefaultYPos, 0f);
        }
    }
}
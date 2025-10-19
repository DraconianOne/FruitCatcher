using UnityEngine;

namespace Player.States
{
    public abstract class PlayerState : MonoBehaviour
    {
        protected static PlayerController _player { get; private set; }
        protected StateEnum _type = StateEnum.None;
        public StateEnum StateType {get{return _type;}}

        protected virtual void Awake()
        {
           _player = PlayerController.Instance;
        }
        
        
        public abstract void DoUpdate();
        public abstract void OnStateEnter();
        public abstract void OnStateExit();

    }
}
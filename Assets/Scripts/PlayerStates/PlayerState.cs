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
           _player = GetComponent<PlayerController>();
        }

        public virtual void OnStateEnter() { Debug.Log("base:OnStateEnter"); }
        public virtual void OnStateExit() { Debug.Log("base:OnStateExit"); }

    }
}
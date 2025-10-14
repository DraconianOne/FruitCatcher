using Player.States;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Player { 
    public class PlayerController : MonoBehaviour
    {


        public GameObject crate;
        private PlayerState _state;
        private PlayerState _prevState;
        private Dictionary<StateEnum, PlayerState> _states;

        private void Awake()
        {
            
            
        }
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Debug.Log("PlayerController::Start");
            _states = GetComponents<PlayerState>().ToDictionary(s => s.StateType, s => s);
            _state = _states[StateEnum.Default];
            _state.OnStateEnter();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void ChangeState(StateEnum newState)
        {
            Debug.Log("PlayerController::ChangeState: " +  newState);
            Debug.Log("Current State: " + _state);
            if (newState == StateEnum.None || !_states.ContainsKey(newState) || _state == _states[newState])
            {
                Debug.LogError("Error changing state");
                return;
            };
        
            if(_state) _state.OnStateExit();
            _prevState = _state;
            _state = _states[newState];
            _state.OnStateEnter();
        }
        
    }
}

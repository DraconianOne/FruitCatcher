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
        private Dictionary<StateEnum, PlayerState> _states =  new Dictionary<StateEnum, PlayerState>();

        private void Awake()
        {
            Debug.Log("PlayerController::Awake");
        }
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            
            Debug.Log("PlayerController::Start");
            
            PlayerState[] statesArray = GetComponents<PlayerState>();
            Debug.Log("StatesArray Length: " + statesArray.Length);
            foreach (var st in statesArray)
            {
               Debug.Log("Adding " + st);
                _states.Add(st.StateType, st);
            }
            //_states = GetComponents<PlayerState>().ToDictionary(s => s.StateType, s => s);
            Debug.Log("Got states: " + _states.Count);
            foreach (var st in _states)
            {
                Debug.Log(st.Key + " - " + st.Value);
            }
            _state = _states[StateEnum.Default];
            
            
            
            /*
            PlayerState[] statesArray = GetComponents<PlayerState>();
            Debug.Log("StatesArray Length: " + statesArray.Length);
            foreach (var st in statesArray)
            {
                Debug.Log(st.ToString());
            }
            _states = GetComponents<PlayerState>().ToDictionary(s => s.StateType, s => s);
            
            _state = _states[StateEnum.Default];
            */
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
            if (newState == StateEnum.None)
            {
                Debug.LogError("newState is NONE");
                return;
            }
            if (!_states.ContainsKey(newState))
            {
                Debug.LogError("States dictionary does not contain " + newState);
                foreach (var st in _states)
                {
                    Debug.Log(st.Key);
                }
                return;
            };
            if (_state == _states[newState])
            {
                Debug.LogError(_state + " is same as " + newState);
                return;
            };
            /*
            if (newState == StateEnum.None || !_states.ContainsKey(newState) || _state == _states[newState])
            {
                Debug.LogError("Error changing state");
                return;
            };
            */
        
            if(_state) _state.OnStateExit();
            _prevState = _state;
            _state = _states[newState];
            _state.OnStateEnter();
        }
        
    }
}

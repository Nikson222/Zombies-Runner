namespace StateMachines
{
    public class StateMachine
    {
        private IState _currentState;

        public IState CurrentState => _currentState;

        public void Initialize(IState initializeState)
        {
            _currentState = initializeState;
            _currentState.Enter();
        }

        public void ChangeState(IState newState)
        {
            _currentState.Exit();

            _currentState = newState;
            
            _currentState.Enter();
        }
    }
}
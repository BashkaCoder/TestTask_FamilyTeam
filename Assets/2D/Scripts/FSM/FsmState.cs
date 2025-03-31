namespace _2D.Scripts.FSM
{
    public abstract class FsmState
    {
        protected readonly Fsm Fsm;

        protected FsmState(Fsm fsm)
        {
            Fsm = fsm;
        }

        public virtual void Enter() {} 
        public virtual void Exit() {}
        public virtual void Update(float deltaTime) {}
    }
}

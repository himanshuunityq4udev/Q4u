using UnityEngine;

namespace state
{
    public class RespawnState : IState
    {
        private BallController ballController;

        public RespawnState(BallController ballController)
        {
            this.ballController = ballController;
        }

        public void Enter()
        {
            Debug.Log("Entered Respawn State");
            ballController.Respawn();
        }

        public void Execute()
        {
            ballController.StateMachine.ChangeState(new PlayingState(ballController));
        }

        public void Exit()
        {
            Debug.Log("Exited Respawn State");
        }
    }
}
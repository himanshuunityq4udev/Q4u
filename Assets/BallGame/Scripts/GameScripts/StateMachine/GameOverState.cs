using UnityEngine;


namespace state
{
    public class GameOverState : IState
    {
        private BallController ballController;

        public GameOverState(BallController ballController)
        {
            this.ballController = ballController;
        }

        public void Enter()
        {
            Debug.Log("Game Over");
            ballController.ShowGameOverScreen();
        }

        public void Execute()
        {   
            if(ballController.playerData.life == ballController.playerData.totalLife)
            {
                ballController.StateMachine.ChangeState(new RespawnState(ballController));
            }
        }

        public void Exit()
        {

        }
    }
}
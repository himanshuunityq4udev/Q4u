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

        public void Execute() { }

        public void Exit() { }
    }
}
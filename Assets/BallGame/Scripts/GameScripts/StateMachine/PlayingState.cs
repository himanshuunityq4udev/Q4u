using UnityEngine;

namespace state
{
    public class PlayingState : IState
    {
        private BallController ballController;

        public PlayingState(BallController ballController)
        {
            this.ballController = ballController;
        }

        public void Enter()
        {
            Debug.Log("Entered Playing State");
            ballController.EnableBallControl(true);
        }

        public void Execute()
        {
            // Gameplay logic (e.g., moving the ball, collecting coins)
            if (ballController.IsAtFinishLine())
            {
                ballController.StateMachine.ChangeState(new WinState(ballController));
            }
            else if (ballController.IsFallen() && !ballController.IsGrounded())
            {
                ballController.LoseLife();

                if (ballController.playerData.life > 0)
                {
                    ballController.StateMachine.ChangeState(new RespawnState(ballController));
                }
                else
                {
                    ballController.StateMachine.ChangeState(new GameOverState(ballController));
                }
            }
        }

        public void Exit()
        {
            Debug.Log("Exited Playing State");
            ballController.EnableBallControl(false);
        }
    }
}
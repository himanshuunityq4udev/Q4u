using UnityEngine;

namespace state
{
    public class WinState : IState
    {
        private BallController ballController;

        public WinState(BallController ballController)
        {
            this.ballController = ballController;
        }

        public void Enter()
        {
            Debug.Log("You Win!");
            ballController.ShowWinScreen();
        }

        public void Execute() {

        }

        public void Exit() { }
    }
}
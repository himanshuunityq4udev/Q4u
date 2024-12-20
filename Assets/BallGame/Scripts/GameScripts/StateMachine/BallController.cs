using UnityEngine;

namespace state
{
    public class BallController : MonoBehaviour
    {
        public float moveSpeed = 10f; // Speed multiplier
        public StateMachine StateMachine { get; private set; }

        [SerializeField] int revivalAmount = 200;

        private Rigidbody rb;

        public PlayerData playerData;

        PlayerInput playerInput;
        [SerializeField] private GameObject cam1;
        [SerializeField] private GameObject cam2;

        bool isLevelComplete;



        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            playerInput = GetComponent<PlayerInput>();
        }




        private void Start()
        {

            StateMachine = new StateMachine();
            StateMachine.ChangeState(new PlayingState(this));
        }

        private void OnEnable()
        {
            ActionHelper.ReviveLife += ReviveLife;
            ActionHelper.GenerateNewLevel += GenerateNextLevel;
        }
        private void OnDisable()
        {
            ActionHelper.ReviveLife -= ReviveLife;
            ActionHelper.GenerateNewLevel -= GenerateNextLevel;

        }

 


        private void Update()
        {
            StateMachine.Update();
        }


        public void EnableBallControl(bool enable)
        {
            rb.isKinematic = !enable;
        }

        public bool IsAtFinishLine()
        {
            return isLevelComplete;
        }

        public bool IsFallen()
        {
            // Check if the ball has fallen below a certain threshold
            return transform.position.y < -10;
        }

        public void LoseLife()
        {
            playerData.life--;
            ActionHelper.updateLifeUI?.Invoke();
            Debug.Log("Lives remaining: " + playerData.life);
        }


        public void ShowWinScreen()
        {
            // Show the win screen UI
            ActionHelper.LevelComplete?.Invoke();
        }

        public void ShowGameOverScreen()
        {
            // Show the game over screen UI
            ActionHelper.LevelFailed?.Invoke();
        }

        private void GenerateNextLevel()
        {
            isLevelComplete = false;
            ActionHelper.GoHome?.Invoke();
            StateMachine.ChangeState(new RespawnState(this));
            playerInput.enabled = true;
            cam1.SetActive(true);
            cam2.SetActive(false);
           
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Finish"))
            {
                Debug.Log("Complete");
                transform.rotation = Quaternion.Euler(0, 0, 0);
                rb.angularVelocity = Vector3.zero;
                rb.linearVelocity = Vector3.zero;
                playerInput.enabled = false;
                cam1.SetActive(false);
                cam2.SetActive(true);
                isLevelComplete = true;
            }
           
        }


        #region ------------RESET THE BALL ---------------

        public void Respawn()
        {
            transform.position = playerData.respawnPosition;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            Debug.Log("Respawned");
        }

        public void ReviveLife()
        {
            if (MoneyManager.Instance.playerInfo.money > revivalAmount)
            {
                MoneyManager.Instance.SpendMoney(revivalAmount);
                playerData.life = playerData.totalLife;
                ActionHelper.updateLifeUI?.Invoke();
            }
            else
            {
                ActionHelper.DontHaveEnoughCoins?.Invoke();
            }
        }
        #endregion
    }

}
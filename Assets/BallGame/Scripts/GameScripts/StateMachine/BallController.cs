using UnityEngine;
using System.Collections;

using Unity.Cinemachine;
namespace state
{
    public class BallController : MonoBehaviour
    {
      //  public float moveSpeed = 10f; // Speed multiplier
        public StateMachine StateMachine { get; private set; }

      //  [SerializeField] int revivalAmount = 200;

        private Rigidbody rb;

        public PlayerData playerData;

        PlayerInput playerInput;
        [SerializeField] private GameObject cam1;

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
            return transform.position.y < -30;
        }

        public bool IsGrounded()
        {
            // Check if the ball is on the ground using a raycast
            return Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 100f);
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
            //ActionHelper.GoHome?.Invoke();
            StateMachine.ChangeState(new RespawnState(this));
            playerInput.enabled = true;
            cam1.GetComponent<Animator>().enabled = false;
            isLevelComplete = false;
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Finish"))
            {
                Debug.Log("Complete");
                transform.rotation = Quaternion.Euler(0, 0, 0);
                rb.angularVelocity = Vector3.zero;
                rb.linearVelocity = Vector3.zero;
                rb.useGravity = false;
                playerInput.enabled = false;
                cam1.GetComponent<Animator>().enabled = true;
                isLevelComplete = true;
                playerData.respawnPosition = new Vector3(0, 1, 0);
            }
        }


        #region ------------RESET THE BALL ---------------

        public void Respawn()
        {
            transform.position = playerData.respawnPosition;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.rotation = Quaternion.identity;
            rb.useGravity = true;
            ResetCameraManually();
            Debug.Log("Respawned");
          
        }

        public void ReviveLife()
        {
            if (MoneyManager.Instance.playerInfo.money > playerData.revivalAmount)
            {
                MoneyManager.Instance.SpendMoney(playerData.revivalAmount);
                playerData.life = playerData.totalLife;
                ActionHelper.updateLifeUI?.Invoke();
            }
            else
            {
                ActionHelper.DontHaveEnoughCoins?.Invoke();
            }
        }

        private void  ResetCameraManually()
        {

            cam1.GetComponent<CinemachineRotationComposer>().enabled = false;
            cam1.GetComponent<CinemachineOrbitalFollow>().enabled = false;

            cam1.transform.position = new Vector3(0, cam1.transform.position.y, transform.position.z - 3.5f);
            cam1.transform.rotation = Quaternion.Euler(cam1.transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

            cam1.GetComponent<CinemachineRotationComposer>().enabled = true;
            cam1.GetComponent<CinemachineOrbitalFollow>().enabled = true;


        }

        #endregion
    }

}
using UnityEngine;
using UnityEngine.UI;

public class ButtonInvoker : MonoBehaviour
{
    //[SerializeField] private string buttonName; // Button identifier, can be set in the Inspector
    [SerializeField] ButtonsName buttonName;

    Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
    }
    private void Start()
    {
        button.onClick.AddListener(OnClick);
    }

    // This method is called when the button is clicked
    public void OnClick()
    {
        ButtonActionManager.ButtonClick(buttonName);
    }
}

using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToggleSwitch : MonoBehaviour
{
    //Refrence to UI element
    public Image fillImage; //background image
    public Image handleImage;//Handle image
    public TMP_Text stateText; //Text display
    public float fillSpeed = 5f;

    //variables
    private RectTransform handle; //Refrence to the handle's RectTransform
    private bool isOn = false; // Current state of the toggle
    private float targetFillamount; // Target fill amount for smooth animation

    //Color for the text in diifferent states
    public Color onColor = new Color(37f / 255f, 37f / 255f, 37f / 255f); //#252525
    public Color offColor = Color.white; //#ffffff


    [Header("Events")]

    [SerializeField] private UnityEvent onToggleOn;
    [SerializeField] private UnityEvent onToggleOff;


    //Set inital state and handle refrence
    private void Start()
    {
        //Get handle RectTransform component
        handle = handleImage.GetComponent<RectTransform>();

        //set pivot of the handle to the left side
        handle.pivot = new Vector2(0f, .5f); //set pivot to left side

        //set initial target fill amount based on the initial state
        targetFillamount = isOn ? 1f : 0f;

        //Update the handle pivot, fill amount, and state text display
        UpdateHandlePivot();
        UpdateFillAmount();
        UpdateStateText();

    }

    //Toggle the Button
    public void Toggle()
    {
        //Change state
        isOn = !isOn;

        //settarget fill amount based on the new state
        targetFillamount = isOn ? 1f : 0f;

        //Update handle pivot and state text display
        UpdateHandlePivot();
        UpdateStateText();
    }

    //Update fill amount smoothly
    private void Update()
    {
        //smoothly update fill amount
        fillImage.fillAmount = Mathf.Lerp(fillImage.fillAmount, targetFillamount, Time.deltaTime * fillSpeed);

        //Move the handle smoothly to the appropriate position
        float PosX = isOn ? fillImage.rectTransform.rect.width : 0f;
        handle.anchoredPosition = new Vector2(Mathf.Lerp(handle.anchoredPosition.x, PosX, Time.deltaTime * 8f), handle.anchoredPosition.y);


    }


    //Update fill amount instantly
    private void UpdateFillAmount()
    {
        //Update fill amount
        fillImage.fillAmount = targetFillamount;

        //update handle posiion instantly
        handle.anchoredPosition = new Vector2(targetFillamount * fillImage.rectTransform.rect.width, handle.anchoredPosition.y);
    }

    //update state text
    private void UpdateStateText()
    {
        stateText.text = isOn ? "ON" : "OFF";
        stateText.color = isOn ? onColor : offColor;
    }
    //updtate handle pivote based on the toggle state
    private void UpdateHandlePivot()
    {
        // set pivot to the right side if the toggle is on otherwise set it to the left side
        if (isOn)
        {
            handle.pivot = new Vector2(1f, 0.5f); //Set pivot to right side
            stateText.GetComponent<RectTransform>().pivot = new Vector2(2f, 0.5f) ;
        }
        else
        {
            handle.pivot = new Vector2(0f, 0.5f); //Set pivot to left side
            stateText.GetComponent<RectTransform>().pivot = new Vector2(1f, 0.5f);
        }
    }

}
using UnityEngine.UIElements;

namespace Q4u
{
    /// <summary>
    /// UI logic for the PauseScreen (PauseScreen.uxml)
    /// </summary>
    public class PauseScreen : UIScreen
    {
        Button m_BackButton;
        Button m_QuitButton;

        public PauseScreen(VisualElement rootElement): base(rootElement)
        {
            m_BackButton = m_RootElement.Q<Button>("back-button");
            m_QuitButton = m_RootElement.Q<Button>("quit-button");

            m_IsTransparent = true;

            m_EventRegistry.RegisterCallback<ClickEvent>(m_BackButton, evt => UIEvents.ScreenClosed());
    

            // Clicking the quit button during gameplay will take the user to the main menu
            m_EventRegistry.RegisterCallback<ClickEvent>(m_QuitButton, evt => UIEvents.MainMenuShown());

        }
    }
}

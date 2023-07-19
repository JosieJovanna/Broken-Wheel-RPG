namespace BrokenWheel.UI.Common
{
    public interface IDisplay
    {
        /// <summary>
        /// Gets the master position of the display.
        /// </summary>
        /// <returns> Item 1: X, Item 2: Y. </returns>
        (int, int) GetPosition();
        
        /// <summary>
        /// Whether the individual display element is currently being rendered.
        /// This will be set as or after <see cref="Hide"/> is called, as there may be a time taken to fade.
        /// </summary>
        bool IsHidden { get; set; }
        
        /// <summary>
        /// Starts showing the display element, possibly with a fade.
        /// </summary>
        void Show();
        
        /// <summary>
        /// Starts hiding the display element, possibly with a fade.
        /// </summary>
        void Hide();
        
        /// <summary>
        /// Sets the master position of the display. Any child elements' position will be relative to this position.
        /// </summary>
        void SetPosition(int x, int y);
    }
}

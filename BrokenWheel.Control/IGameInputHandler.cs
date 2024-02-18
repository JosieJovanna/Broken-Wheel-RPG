using BrokenWheel.Core.Events.Handling;
using BrokenWheel.Core.GameModes;

namespace BrokenWheel.Control
{
    /// <summary>
    /// An object in charge of keeping track of raw player input.
    /// Translates into BrokenWheel input and is responsible for calling the <see cref="IRPGInputTracker"/> when processing.
    /// </summary>
    /// <typeparam name="TGameInputEvent"> The type of action which is used on the implementation level. </typeparam>
    public interface IGameInputHandler<TGameInputEvent> : IEventHandler<GameModeUpdateEvent>
    {
        /// <summary>
        /// Processes game engine input mapping, in case it has been changed via in-game settings.
        /// </summary>
        void RefreshInputMap();

        /// <summary>
        /// Process results of input.
        /// </summary>
        /// <param name="delta"> The amount of time passed since the last <see cref="Process(double)"/> call. </param>
        void Process(double delta);

        /// <summary>
        /// Caches current state of inputs to be passed forward as a translated <see cref="Enum.RPGInput"/> or <see cref="Enum.UIInput"/>.
        /// </summary>
        /// <param name="inputEvent"> The input event native to the game engine implementing this RPG system. </param>
        void HandleInput(TGameInputEvent inputEvent);
    }
}

using BrokenWheel.Core.Events.Handling;
using BrokenWheel.Core.GameModes;
using BrokenWheel.Core.Time.Listeners;

namespace BrokenWheel.Control
{
    /// <summary>
    /// An object in charge of keeping track of raw player input.
    /// Translates into BrokenWheel input and is responsible for calling the <see cref="IRPGInputTracker"/> when processing.
    /// </summary>
    /// <typeparam name="TGameInputEvent"> The type of action which is used on the implementation level. </typeparam>
    public interface IGameInputHandler<TGameInputEvent> 
        : IEventHandler<GameModeUpdateEvent>
        , IOnTickTime
    {
        /// <summary>
        /// Processes game engine input mapping, in case it has been changed via in-game settings.
        /// </summary>
        void RefreshInputMap();

        /// <summary>
        /// Caches current state of inputs to be passed forward as a translated <see cref="Enum.RPGInput"/> or <see cref="Enum.UIInput"/>.
        /// </summary>
        /// <param name="inputEvent"> The input event native to the game engine implementing this RPG system. </param>
        void HandleInput(TGameInputEvent inputEvent);
    }
}

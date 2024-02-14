using System.Collections.Generic;
using BrokenWheel.Control.Enum;

namespace BrokenWheel.Control.Interfaces
{
    /// <summary>
    /// Handles translating the game engine input into RPG input data.
    /// </summary>
    /// <typeparam name="TGameInputEvent"> The engine-level input event. </typeparam>
    public interface IGameInputMapper<TGameInputEvent>
    {
        /// <summary>
        /// Refreshes the input mapping from game engine level to RPG system level.
        /// </summary>
        void RefreshInputMap();

        /// <summary>
        /// Translates the engine-level input event into a <see cref="RPGInput"/>
        /// </summary>
        /// <param name="inputEvent"></param>
        /// <returns> The mapped RPG input. </returns>
        IEnumerable<RPGInput> MapToRpgInput(TGameInputEvent inputEvent);
    }
}

using BrokenWheel.Control.Enum;

namespace BrokenWheel.Control.Puppeteering
{
    /// <summary>
    /// Translates game input into actions and movements.
    /// Allows for interchangeable <see cref="IMarionette"/>s as well as additional listeners.
    /// </summary>
    public interface IMarionetteControl
    {
        /// <summary>
        /// The current stance which all marionettes are in, or trying to be.
        /// </summary>
        MovementStance Stance { get; }

        /// <summary>
        /// The current speed which all marionettes are in, or trying to be.
        /// </summary>
        MovementSpeed Speed { get; }

        /// <summary>
        /// Sets the primary <see cref="IMarionette"/> to be controlled by the player.
        /// Setting this removes any existing main marionette.
        /// </summary>
        void SetMainMarionette(IMarionette marionette);

        /// <summary>
        /// Adds an additional <see cref="IMarionette"/> to listen to the player's control.
        /// </summary>
        void AddSideMarionette(IMarionette marionette);

        /// <summary>
        /// Removes the <see cref="IMarionette"/> from listening to the player's control.
        /// </summary>
        void CutSideMarionette(IMarionette marionette);

        /// <summary>
        /// Removes all additional <see cref="IMarionette"/>s, and not the main one.
        /// </summary>
        void CutAllSideMarionettes();

        /// <summary>
        /// Removes all <see cref="IMarionette"/>s from listening to the player's control.
        /// </summary>
        void CutAllStrings();

        /// <summary>
        /// Moves all marionettes.
        /// </summary>
        void Move(float horizontal, float vertical, float delta);

        /// <summary>
        /// Rotates all marionettes.
        /// </summary>
        void Look(float horizontal, float vertical, float delta);

        /// <summary>
        /// Makes all marionettes jump (if able).
        /// </summary>
        void Jump(float strength);

        /// <summary>
        /// Sets the current stance of all marionettes (if able).
        /// </summary>
        void SetStance(MovementStance stance);

        /// <summary>
        /// Sets the current speed of all marionettes (if able).
        /// </summary>
        void SetSpeed(MovementSpeed speed);
    }
}

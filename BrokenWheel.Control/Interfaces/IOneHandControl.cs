﻿using BrokenWheel.Control.Models;

namespace BrokenWheel.Control.Interfaces
{
    /// <summary>
    /// The object which maps input to items for use. Behaviors vary greatly by item.
    /// </summary>
    public interface IOneHandControl
    {
        /// <summary>
        /// Attempts to perform the primary action(s) of the implement, which may depend on the type of button press.
        /// </summary>
        /// <returns>Whether or not any action could be taken.</returns>
        void TryPrimary(PressData press);
    }
}

using System;
using BrokenWheel.Control.Models;
using BrokenWheel.Core.Logging;

namespace BrokenWheel.Control.Implementations.GameInputHandler
{
    public abstract class AbstractGameInputHandler<TGameInputEvent> : IGameInputHandler<TGameInputEvent>
    {
        private readonly IRPGInputHandler _rpgInputHandler;

        protected readonly ILogger Logger;
        protected readonly IGameInputMapper<TGameInputEvent> Mapper;

        public AbstractGameInputHandler(
            ILogger logger,
            IGameInputMapper<TGameInputEvent> mapper,
            IRPGInputHandler rpgInputHandler)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _rpgInputHandler = rpgInputHandler ?? throw new ArgumentNullException(nameof(rpgInputHandler));
        }

        public void RefreshInputMapping() => Mapper.RefreshInputMap();

        public void Process(double delta) => _rpgInputHandler.Process(delta);

        public void HandleInput(TGameInputEvent inputEvent) => _rpgInputHandler.HandleInput(GameEventToInputData(inputEvent));

        /// <summary>
        /// Translates the engine-level input event into <see cref="InputData"/> to be used by the RPG system.
        /// </summary>
        /// <param name="inputEvent"> Engine-level game input event. </param>
        /// <returns> Translated RPG input. </returns>
        protected abstract InputData GameEventToInputData(TGameInputEvent inputEvent);
    }
}

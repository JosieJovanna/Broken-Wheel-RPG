using System;
using System.Collections.Generic;
using System.Linq;
using BrokenWheel.Control.Actions;
using BrokenWheel.Control.Behaviors;
using BrokenWheel.Control.Extensions;
using BrokenWheel.Control.Interfaces;
using BrokenWheel.Control.Models;
using BrokenWheel.Core.Settings;

namespace BrokenWheel.Control.Implementations
{
    /// <summary>
    /// Specifically handles the logic of reloading and readying weapons, since they implement a behavior by default.
    /// </summary>
    public class ReloadOrReadyController
    {
        private readonly EquipmentControl _equipment;
        private readonly IActionBehavior _behavior;

        public ReloadOrReadyController(EquipmentControl equipment)
        {
            _equipment = equipment ?? throw new ArgumentNullException(nameof(equipment));
            _behavior = CreateBehavior();
        }

        private ModMuxBehavior CreateBehavior()
        {
            throw new NotImplementedException();
            /*return new AltMuxBehavior(ref StaticSettings.Controls.HoldToReadyWeapon) 
            {
                OnReleaseHold = ReadyWeapon,
                OnAltReleaseHold = ReadyWeapon,
                OnReleaseClick = Reload,
                OnAltReleaseClick = AltReload
            };*/
        }

        public void ReloadOrReady(PressData press) => throw new NotImplementedException(); //_behavior.Execute(press);

        private void ReadyWeapon() => _equipment.Equipment.ToggleWeaponsReady();

        private void Reload()
        {
            var reloadableList = GetReloadableControls();
            if (!reloadableList.Any())
                return;

            // TODO
        }

        private void AltReload()
        {

        }

        private void TryFirstThenSecond(IOneHandControl ctrlOne, IOneHandControl ctrlTwo)
        {
            var shouldTryTwo = TryReloadingIfOtherDoesntOverride(ctrlOne, ctrlTwo);


        }

        private bool TryReloadingIfOtherDoesntOverride(IOneHandControl ctrlMain, IOneHandControl ctrlOther)
        {
            return false;
        }

        private List<IReloadableControl> GetReloadableControls()
        {
            var list = new List<IReloadableControl>();
            AddToListIfReloadable(list, _equipment.MainHand);
            AddToListIfReloadable(list, _equipment.OffHand);
            return list.Where(rc => rc.CanReload).ToList();
        }

        private static void AddToListIfReloadable(ICollection<IReloadableControl> list, IOneHandControl control)
        {
            if (control.TryCastToInterface<IReloadableControl>(out var asReloadable))
                list.Add(asReloadable);
        }
    }
}

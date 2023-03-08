using System;
using System.Linq;
using System.Collections.Generic;
using LorendisCore.Settings;
using LorendisCore.Control.Models;
using LorendisCore.Control.Behaviors;
using LorendisCore.Control.Implements;

namespace LorendisCore.Control
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

        private AltMuxBehavior CreateBehavior()
        {
            return new AltMuxBehavior(ref StaticSettings.Controls.HoldToReadyWeapon) 
            {
                OnReleaseHold = ReadyWeapon,
                OnAltReleaseHold = ReadyWeapon,
                OnReleaseClick = Reload,
                OnAltReleaseClick = AltReload
            };
        }

        public void ReloadOrReady(PressData press) => _behavior.Execute(press);

        private void ReadyWeapon() => _equipment.Equipment.ToggleWeaponsReady();

        private void Reload()
        {
            var reloadableList = GetReloadableControls();
            if (!reloadableList.Any())
                return;
            
            
        }

        private void AltReload()
        {
            
        }

        private void TryFirstThenSecond(IBaseControl ctrlOne, IBaseControl ctrlTwo)
        {
            var shouldTryTwo = TryReloadingIfOtherDoesntOverride(ctrlOne, ctrlTwo);


        }

        private bool TryReloadingIfOtherDoesntOverride(IBaseControl ctrlMain, IBaseControl ctrlOther)
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

        private static void AddToListIfReloadable(ICollection<IReloadableControl> list, IBaseControl control)
        {
            if (control.TryCastToInterface<IReloadableControl>(out var asReloadable))
                list.Add(asReloadable);
        }
    }
}
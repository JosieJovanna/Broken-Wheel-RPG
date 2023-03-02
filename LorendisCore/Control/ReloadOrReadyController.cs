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
            return new AltMuxBehavior(StaticSettings.Controls.HoldToReadyWeapon) // TODO: make it so the control can actually change without spinning up new control objects?
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
            if (!CanAnyBeReloadedInList(reloadableList))
                return;
            
            foreach (var reloadable in reloadableList)
            {
                
            }
        }

        private void AltReload()
        {
            
        }

        private static bool CanAnyBeReloadedInList(IReadOnlyCollection<IReloadableControl> reloadableList)
        {
            if (reloadableList == null || !reloadableList.Any())
                return false;
            var areAnyReloadable = reloadableList.Any(r => r.CanReload);
            var noneAreReloading = reloadableList.All(r => !r.IsReloading);
            return areAnyReloadable && noneAreReloading;
        }

        private List<IReloadableControl> GetReloadableControls()
        {
            var list = new List<IReloadableControl>();
            AddToListIfReloadable(list, _equipment.MainHand);
            AddToListIfReloadable(list, _equipment.OffHand);
            return list;
        }

        private static void AddToListIfReloadable(ICollection<IReloadableControl> list, IBaseControl control)
        {
            if (control.TryCastToInterface<IReloadableControl>(out var asReloadable))
                list.Add(asReloadable);
        }
    }
}
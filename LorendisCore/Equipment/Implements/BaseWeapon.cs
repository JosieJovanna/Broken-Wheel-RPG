﻿using LorendisCore.Player.Control.Actions;
using LorendisCore.Player.Control.Actions.Models;

namespace LorendisCore.Equipment.Implements
{
    public abstract class BaseWeapon
    {
        public ActionBehaviorMap Behaviors;

        protected IActionController Controller;

        private bool _isInOffhand = false;

        public BaseWeapon(IActionController actionController)
        {
            Controller = actionController;
        }

        public bool IsInOffhand() => _isInOffhand;

        public void Equip(bool offhanded = false)
        {
            _isInOffhand = offhanded;
        }

        protected abstract void EquipWeapon();
    }
}

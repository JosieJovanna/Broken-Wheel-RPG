﻿using BrokenWheel.Control;
using BrokenWheel.Core.Stats.Processing;

namespace BrokenWheel.Entity
{
    public abstract class AbstractEntity
    {
        public IActionController ActionController;
        public IStatBox Stats;
    }
}
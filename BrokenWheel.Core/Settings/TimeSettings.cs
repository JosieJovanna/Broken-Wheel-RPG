using System;
using System.Collections.Generic;
using System.Text;

namespace BrokenWheel.Core.Settings
{
    public partial class TimeSettings : ISettings
    {
        public float DefaultTimeScale { get; set; } = 1.0f;
        public float DefaultCalendarTimeScale { get; set; } = 0.1f;
    }
}

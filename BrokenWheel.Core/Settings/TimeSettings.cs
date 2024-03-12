namespace BrokenWheel.Core.Settings
{
    public partial class TimeSettings : ISettings
    {
        public float DefaultTimeScale { get; set; } = 1.0f;
        public float DefaultCalendarTimeScale { get; set; } = 60f;
    }
}

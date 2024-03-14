using BrokenWheel.Core.DependencyInjection;
using BrokenWheel.Core.Events.Attributes;
using BrokenWheel.Core.Settings;

namespace BrokenWheel.Core.Events
{
    public partial class SettingsUpdateEvent<TSettings> where TSettings : class, ISettings
    {
        [DefaultEventGetter]
        public static SettingsUpdateEvent<TSettings> Default()
        {
            var settings = Injection.GetModule().GetSettings<TSettings>();
            return new SettingsUpdateEvent<TSettings>(settings);
        }

        /// <summary>
        /// A reference to the edited settings file, in case the reference is lost.
        /// </summary>
        public TSettings Settings { get; }

        public SettingsUpdateEvent(TSettings settings)
        {
            Settings = settings;
        }
    }
}

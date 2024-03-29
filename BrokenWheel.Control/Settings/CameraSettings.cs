﻿using BrokenWheel.Core.Settings;
using BrokenWheel.Math.Utility;

namespace BrokenWheel.Control.Settings
{
    public partial class CameraSettings : ISettings
    {
        /// <summary>
        /// The base speed at which the camera rotates.
        /// </summary>
        public float BaseSpeed { get; set; } = 0.02f; // TODO: move/hide?

        public float PanSensitivity { get; set; } = 1.7f;
        public float TiltSensitivity { get; set; } = 1.5f;
        public float HeightOffset { get; set; } = -0.05f; // TODO: move?


        public bool InvertVertical { get; set; } = false;
        public float TiltMinimum { get; set; } = AngleUtil.DegreesToRadians(-85);
        public float TiltMaximum { get; set; } = AngleUtil.DegreesToRadians(90); // TODO: move to config? Or just hide?

        public float InterpolateUpSpeed { get; set; } = 4.85f;
        public float InterpolateDownSpeed { get; set; } = 2.85f;
        public float CrouchHeightFactor { get; set; } = 0.5f;
        public float CrawlHeight { get; set; } = 0.5f;

        public float FOVNormal { get; set; } = 80.0f;
        public float FOVSprinting { get; set; } = 90.0f;
    }
}

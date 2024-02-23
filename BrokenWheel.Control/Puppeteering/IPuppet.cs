namespace BrokenWheel.Control.Puppeteering
{
    /// <summary>
    /// An interface implemented by anything that moves.
    /// </summary>
    public interface IPuppet : IRPGEntity
    {
        /// <summary>
        /// Moves the puppet's physical body on a 2D plane relative to its current rotation and location.
        /// </summary>
        /// <param name="vDolly"> The front-to-back base velocity. Positive moves forward. </param>
        /// <param name="vTruck"> The side-to-side base velocity. Positive moves right. </param>
        /// <param name="delta"> The amount of time passed this tick. </param>
        void Move(float vDolly, float vTruck, float delta);

        /// <summary>
        /// Rotates the puppet's body and/or camera relative to its current rotation.
        /// </summary>
        /// <param name="tilt"> The up/down rotation. Positive looks up. </param>
        /// <param name="pan"> The left/right rotation. Positive looks clockwise (right). </param>
        void Look(float tilt, float pan, float delta);

        /// <summary>
        /// Rotates the puppet's body and/or camera to look at the specified game coordinates.
        /// </summary>
        void LookAt(float x, float y, float z);
    }
}

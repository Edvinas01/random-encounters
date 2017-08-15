namespace RandomEncounters
{
    public static class Constants
    {
        /// <summary>
        /// Rendering frame rate.
        /// </summary>
        public const int Fps = 100;

        /// <summary>
        /// The amount of pixels the image can move per tick.
        /// </summary>
        public const int MoveSpeed = 10;
        
        /// <summary>
        /// How often to display the image (in seconds). Also note that there is a random offset added to the interval.
        /// </summary>
        public const int Interval = 1;

        /// <summary>
        /// Bouncyness of the image when its moving along the screen.
        /// </summary>
        public const float Bounciness = 0.2f;

        /// <summary>
        /// Name of the application once it is added to startup reg key and copied over to the roaming folder.
        /// </summary>
        public const string AppName = "RandomEncounters";
    }
}
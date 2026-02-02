namespace ProjectBlocky.Utils
{
    public static class Constants
    {
        /// <summary>
        /// The GDD Technical Information part states 128x128 pixels, since we're importing the sprites
        /// as 1 unit = 100 pixels, each cell will be 1.28x1.28 units.
        /// </summary>
        public const float CELL_WIDTH = 1.28f;
        public const float CELL_HEIGHT = 1.28f;

        /// <summary>
        /// See GDD task 2 point 3 to reference this value.
        /// </summary>
        public const int POINT_MULTIPLIER = 10;

        /// <summary>
        /// See GDD Technical Information point to know more about grid dimensions.
        /// </summary>
        public const int ROWS = 6;
        public const int COLUMNS = 5;

        /// <summary>
        /// See GDD task 3 point 5 for more information about this value.
        /// </summary>
        public const float AWAIT_RESOLUTION = 1f;
    }
}
using static DxLibDLL.DX;

namespace DxLibUI
{
    internal class Image
    {
        public readonly int[] mapchips = null;

        public readonly int[] charachips = null;

        public readonly int selectSign;

        public readonly int movableSign;

        public readonly int attackableSign;

        public readonly int frame;

        private static Image instance;

        private static object lockObj = new object();

        private Image()
        {
            mapchips = new int[16 * 12];
            LoadDivGraph("./Resources/mapchip.png", 16*12, 16,12, 30, 30, mapchips);
            charachips = new int[12 * 8];
            LoadDivGraph("./Resources/CharaChip.png", 12 * 8, 12, 8, 32, 32, charachips);
            selectSign = LoadGraph("./Resources/SelectSign.png");
            frame = LoadGraph("./Resources/Frame.png");
            movableSign = LoadGraph("./Resources/MovableSign.png");
            attackableSign = LoadGraph("./Resources/AttackableSign.png");
        }

        public static Image GetInstance()
        {
            lock(lockObj)
            {
                if (instance == null)
                {
                    instance = new Image();
                }
                return instance;
            }
        }

        internal class Constants
        {
            public static readonly int MAPCHIP_WIDTH = 40;

            public static readonly int MAPCHIP_HEIGHT = 40;

        }


    }
}

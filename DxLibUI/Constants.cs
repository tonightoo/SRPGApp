using Domain.Models;
using static DxLibDLL.DX;

namespace DxLibUI
{
    internal class Constants
    {

        internal class Turn
        {
            public static readonly string COM_TURN_START_TEXT = "COM TURN";

            public static readonly string PLAYER_TURN_START_TEXT = "PLYER TURN";

            public static readonly int TOPLEFT_X = 50;

            public static readonly int TOPLEFT_Y = 100;

            public static readonly int FONT_SIZE = 100;
        }


        internal class Color
        {
            public static readonly uint ENABLED_COLOR = GetColor(255, 255, 255);

            public static readonly uint DISABLED_COLOR = GetColor(128, 128, 128);
        }

        internal class Window
        {
            public static readonly int WIDTH = 800;

            public static readonly int HEIGHT = 600;

            public static readonly int COLOR_BIT_NUM = 32;
        }

        internal class UnitInfo
        {
            public static readonly int TOPLEFT_X = 50;

            public static readonly int TOPLEFT_Y = 420;

            public static readonly int WIDTH = 700;

            public static readonly int HEIGHT = 150;

            public static readonly byte TRANSPARENCY = 200;

            public static readonly int[] colX = { 50, 200, 350, 500 };

            public static readonly int[] rowY = { 20, 50, 90 };

            public class Name
            {
                public static readonly int X = TOPLEFT_X + colX[0];

                public static readonly int Y = TOPLEFT_Y + rowY[0];
            }


            public class HP
            {
                public static readonly string LABEL = "HP:";

                public static readonly int X = TOPLEFT_X + colX[0];

                public static readonly int Y = TOPLEFT_Y + rowY[1];
            }

            public class MP
            {
                public static readonly string LABEL = "MP:";

                public static readonly int X = TOPLEFT_X + colX[0];

                public static readonly int Y = TOPLEFT_Y + rowY[2];
 
            }

            public class AT
            {
                public static readonly string LABEL = "AT:";

                public static readonly int X = TOPLEFT_X + colX[1];

                public static readonly int Y = TOPLEFT_Y + rowY[1];
            }

            public class DF
            {
                public static readonly string LABEL = "DF:";

                public static readonly int X = TOPLEFT_X + colX[1];

                public static readonly int Y = TOPLEFT_Y + rowY[2];
            }

            public class MAT
            {
                public static readonly string LABEL = "MAT:";

                public static readonly int X = TOPLEFT_X + colX[2];

                public static readonly int Y = TOPLEFT_Y + rowY[1];
            }

            public class MDF
            {
                public static readonly string LABEL = "MDF:";

                public static readonly int X = TOPLEFT_X + colX[2];

                public static readonly int Y = TOPLEFT_Y + rowY[2];
            }

            public class TEC
            {
                public static readonly string LABEL = "TEC:";

                public static readonly int X = TOPLEFT_X + colX[3];

                public static readonly int Y = TOPLEFT_Y + rowY[1];
            }

            public class LUC
            {
                public static readonly string LABEL = "LUC:";

                public static readonly int X = TOPLEFT_X + colX[3];

                public static readonly int Y = TOPLEFT_Y + rowY[2];
            }

        }

        internal class Command
        {
            public static readonly int TOPLEFT_X = 600;

            public static readonly int TOPLEFT_Y = 20;

            public static readonly int WIDTH = 150;

            public static readonly int HEIGHT = 400;

            public static readonly byte TRANSPARENCY = 200;

            public static readonly int[] rowY = { 50, 100, 150 };

            public static readonly int[] colX = { 10, 30 };

            public static readonly string ALLOW = "→";
            public class Move
            {
                public static readonly string LABEL = "移動";

                public static readonly int X = TOPLEFT_X + colX[1];

                public static readonly int Y = TOPLEFT_Y + rowY[(int) CommandPanel.Command.MoveCommand];
            }

            public class Attack
            {
                public static readonly string LABEL = "攻撃";

                public static readonly int X = TOPLEFT_X + colX[1];

                public static readonly int Y = TOPLEFT_Y + rowY[(int) CommandPanel.Command.AttackCommand];
            }

            public class TurnEnd
            {
                public static readonly string LABEL = "ターン終了";

                public static readonly int X = TOPLEFT_X + colX[1];

                public static readonly int Y = TOPLEFT_Y + rowY[(int) CommandPanel.Command.TurnEndCommand];
            }

        }


    }
}

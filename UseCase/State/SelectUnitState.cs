using Domain;
using Domain.Models;
using System;
using System.Drawing;
using System.Linq;

namespace UseCase.State
{
    public class SelectUnitState
        : IArenaState
    {
    
        /// <summary>
        /// 選択中のマスを上に
        /// </summary>
        /// <param name="arena"></param>
        public void Up(Arena arena)
        {
            int y = arena.cursorPoint.Y - 1;

            if (y >= 0)
            {
                arena.cursorPoint.Y = y;
            }

            if (Math.Abs(arena.cameraPoint.Y - arena.cursorPoint.Y) > Constants.Arena.CAMERA_RANGE)
            {
                arena.cameraPoint.Y -= 1;
            }

        }

        /// <summary>
        /// 選択中のマスを下に
        /// </summary>
        /// <param name="arena"></param>
        public void Down(Arena arena)
        {
            int x = arena.cursorPoint.X;
            int y = arena.cursorPoint.Y + 1;

            if (y < arena.map[x].Count)
            {
                arena.cursorPoint.Y = y;
            }

            if (Math.Abs(arena.cursorPoint.Y - arena.cameraPoint.Y) > Constants.Arena.CAMERA_RANGE)
            {
                arena.cameraPoint.Y += 1;
            }
        }

        /// <summary>
        /// 選択中のマスを左に
        /// </summary>
        /// <param name="arena"></param>
        public void Left(Arena arena)
        {
            int x = arena.cursorPoint.X - 1;

            if (x >= 0)
            {
                arena.cursorPoint.X = x;
            }

            if (Math.Abs(arena.cameraPoint.X - arena.cursorPoint.X) > Constants.Arena.CAMERA_RANGE)
            {
                arena.cameraPoint.X -= 1;
            }
        }

        /// <summary>
        /// 選択中のマスを右に
        /// </summary>
        /// <param name="arena"></param>
        public void Right(Arena arena)
        {
            int x = arena.cursorPoint.X + 1;

            if (x < arena.map.countX)
            {
                arena.cursorPoint.X = x;
            }

            if (Math.Abs(arena.cursorPoint.X - arena.cameraPoint.X) > Constants.Arena.CAMERA_RANGE)
            {
                arena.cameraPoint.X += 1;
            }
        }

        /// <summary>
        /// 選択中のマスにいる味方ユニットのコマンドを表示
        /// </summary>
        /// <param name="arena"></param>
        public void Enter(Arena arena)
        {
            Unit unit = arena.GetUnderCursorUnit();

            //選択中のユニットのチームがプレイヤーのもののとき
            if (unit?.TeamId == Domain.Constants.Team.PLAYER_TEAM_ID)
            {
                arena.selectedPoint = new Point(arena.cursorPoint.X, arena.cursorPoint.Y);
                arena.selectedUnit = unit;
                arena.state = new SelectCommandState();
                arena.commandPanel.selectedIndex = 0;
            }
        }

        public void Escape(Arena arena)
        {
            if (arena.history.Count != 0)
            {
                arena.history.Last().Redo(arena);
            }
        }


    }
}

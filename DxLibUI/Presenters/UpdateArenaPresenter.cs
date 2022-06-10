using System.Drawing;
using Domain.Models;
using UseCase.UpdateArena;
using DxLibUI.Views;
using UseCase.State;
using static DxLibDLL.DX;


namespace DxLibUI.Presenters
{
    internal class UpdateArenaPresenter
        : IUpdateArenaPresenter
    {

        private ViewUpdater _updater;


        public UpdateArenaPresenter(ViewUpdater updater)
        {
            this._updater = updater;
        }

        public void Complete(Arena arena)
        {
            ViewModel viewModel = new ViewModel();

            int graphId;
            int x;
            int y;
            int width = Image.Constants.MAPCHIP_WIDTH;
            int height = Image.Constants.MAPCHIP_HEIGHT;
            byte transparency;


            //マップエリアの描画
            for (int i = 0; i < arena.map.countX; i++)
            {
                for (int j = 0; j < arena.map[i].Count; j++)
                {
                    //マップチップの描画
                    int imageId = arena.map[i][j].ImageId;

                    graphId = Image.GetInstance().mapchips[imageId];
                    x = i * width;
                    y = j * height;

                    Graph graph = new Graph(graphId, x, y, width, height);
                    viewModel.graphs.Add(graph);

                    if (arena.map[i][j].Unit is null)
                    {
                        continue;
                    }

                    //ユニットの描画
                    imageId = arena.map[i][j].Unit.ImageId;
                    graphId = Image.GetInstance().charachips[imageId];

                    Graph unitGraph = new Graph(graphId, x, y, width, height);
                    viewModel.graphs.Add(unitGraph);
                }
            }

            //選択マスの描画
            graphId = Image.GetInstance().selectSign;
            x = arena.cursorPoint.X * width;
            y = arena.cursorPoint.Y * height;

            Graph selectGraph = new Graph(graphId, x, y, width, height);
            viewModel.graphs.Add(selectGraph);

            Unit unit = arena.GetUnderCursorUnit();
            string content;
            uint color;

            //ユニットがいる場合はユニット情報欄を表示
            if (!(unit is null))
            {

                graphId = Image.GetInstance().frame;
                x = Constants.UnitInfo.TOPLEFT_X;
                y = Constants.UnitInfo.TOPLEFT_Y;
                width = Constants.UnitInfo.WIDTH;
                height = Constants.UnitInfo.HEIGHT;
                transparency = Constants.UnitInfo.TRANSPARENCY;

                viewModel.graphs.Add(new Graph(graphId, x, y, width, height, true, transparency));

                //Name
                x = Constants.UnitInfo.Name.X;
                y = Constants.UnitInfo.Name.Y;
                viewModel.texts.Add(new Text(unit.Name, x, y, Constants.Color.ENABLED_COLOR));

                //HP
                content = Constants.UnitInfo.HP.LABEL + " " + unit.CurrentHp + "/" + unit.MaxHp;
                x = Constants.UnitInfo.HP.X;
                y = Constants.UnitInfo.HP.Y;
                viewModel.texts.Add(new Text(content, x, y, Constants.Color.ENABLED_COLOR));

                //MP
                content = Constants.UnitInfo.MP.LABEL + " " + unit.CurrentMp + "/" + unit.MaxMp;
                x = Constants.UnitInfo.MP.X;
                y = Constants.UnitInfo.MP.Y;
                viewModel.texts.Add(new Text(content, x, y, Constants.Color.ENABLED_COLOR));

                //AT
                content = Constants.UnitInfo.AT.LABEL + " " + unit.Attack;
                x = Constants.UnitInfo.AT.X;
                y = Constants.UnitInfo.AT.Y;
                viewModel.texts.Add(new Text(content, x, y, Constants.Color.ENABLED_COLOR));

                //DF
                content = Constants.UnitInfo.DF.LABEL + " " + unit.Deffence;
                x = Constants.UnitInfo.DF.X;
                y = Constants.UnitInfo.DF.Y;
                viewModel.texts.Add(new Text(content, x, y, Constants.Color.ENABLED_COLOR));

                //MAT
                content = Constants.UnitInfo.MAT.LABEL + " " + unit.MagicAttack;
                x = Constants.UnitInfo.MAT.X;
                y = Constants.UnitInfo.MAT.Y;
                viewModel.texts.Add(new Text(content, x, y, Constants.Color.ENABLED_COLOR));

                //MDF
                content = Constants.UnitInfo.MDF.LABEL + " " + unit.MagicDeffence;
                x = Constants.UnitInfo.MDF.X;
                y = Constants.UnitInfo.MDF.Y;
                viewModel.texts.Add(new Text(content, x, y, Constants.Color.ENABLED_COLOR));

                //TEC
                content = Constants.UnitInfo.TEC.LABEL + " " + unit.Technic;
                x = Constants.UnitInfo.TEC.X;
                y = Constants.UnitInfo.TEC.Y;
                viewModel.texts.Add(new Text(content, x, y, Constants.Color.ENABLED_COLOR));

                //LUC
                content = Constants.UnitInfo.LUC.LABEL + " " + unit.Luck;
                x = Constants.UnitInfo.LUC.X;
                y = Constants.UnitInfo.LUC.Y;
                viewModel.texts.Add(new Text(content, x, y, Constants.Color.ENABLED_COLOR));

            }

            if (arena.state is SelectCommandState)
            {
                unit = arena.selectedUnit;

                graphId = Image.GetInstance().frame;
                x = Constants.Command.TOPLEFT_X;
                y = Constants.Command.TOPLEFT_Y;
                width = Constants.Command.WIDTH;
                height = Constants.Command.HEIGHT;
                transparency = Constants.Command.TRANSPARENCY;

                viewModel.graphs.Add(new Graph(graphId, x, y, width, height, true, transparency));

                //移動
                content = Constants.Command.Move.LABEL;
                x = Constants.Command.Move.X;
                y = Constants.Command.Move.Y;

                if (unit.IsMoved)
                {
                    color = Constants.Color.DISABLED_COLOR;
                }
                else
                {
                    color = Constants.Color.ENABLED_COLOR;
                }
                viewModel.texts.Add(new Text(content, x, y, color));

                //攻撃
                content = Constants.Command.Attack.LABEL;
                x = Constants.Command.Attack.X;
                y = Constants.Command.Attack.Y;

                if (unit.IsAttacked)
                {
                    color = Constants.Color.DISABLED_COLOR;
                }
                else
                {
                    color = Constants.Color.ENABLED_COLOR;
                }

                viewModel.texts.Add(new Text(content, x, y, color));

                //ターン終了
                content = Constants.Command.TurnEnd.LABEL;
                x = Constants.Command.TurnEnd.X;
                y = Constants.Command.TurnEnd.Y;
                viewModel.texts.Add(new Text(content, x, y, Constants.Color.ENABLED_COLOR));

                //選択
                content = Constants.Command.ALLOW;
                x = Constants.Command.TOPLEFT_X + Constants.Command.colX[0];
                y = Constants.Command.TOPLEFT_Y + Constants.Command.rowY[arena.commandPanel.selectedIndex];
                viewModel.texts.Add(new Text(content, x, y, Constants.Color.ENABLED_COLOR));
            }

            //移動範囲
            if (arena.movablePoints.Count > 0)
            {
                width = Image.Constants.MAPCHIP_WIDTH;
                height = Image.Constants.MAPCHIP_HEIGHT;
                foreach (Point point in arena.movablePoints)
                {
                    x = point.X * width;
                    y = point.Y * height;
                    Graph graph = new Graph(Image.GetInstance().movableSign, x, y, width, height, true);
                    viewModel.graphs.Add(graph);
                }
            }

            if (arena.attackablePoints.Count > 0)
            {
                width = Image.Constants.MAPCHIP_WIDTH;
                height = Image.Constants.MAPCHIP_HEIGHT;
                foreach (Point point in arena.attackablePoints)
                {
                    x = point.X * width;
                    y = point.Y * height;
                    Graph graph = new Graph(Image.GetInstance().attackableSign, x, y, width, height, true);
                    viewModel.graphs.Add(graph);
                }
            }

            int fontSize = Constants.Turn.FONT_SIZE;

            if (arena.state is ComTurnStart)
            {
                content = Constants.Turn.COM_TURN_START_TEXT;
                x = Constants.Turn.TOPLEFT_X;
                y = Constants.Turn.TOPLEFT_Y;
                color = Constants.Color.ENABLED_COLOR;

                viewModel.texts.Add(new Text(content, x, y, color, fontSize));
            }

            if (arena.state is ComTurnEnd)
            {
                content = Constants.Turn.PLAYER_TURN_START_TEXT;
                x = Constants.Turn.TOPLEFT_X;
                y = Constants.Turn.TOPLEFT_Y;
                color = Constants.Color.ENABLED_COLOR;

                viewModel.texts.Add(new Text(content, x, y, color, fontSize));
            }


            _updater.CreateViewModel = viewModel;
        }
    }
}

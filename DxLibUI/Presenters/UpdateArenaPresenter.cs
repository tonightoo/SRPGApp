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

        private ViewModel _viewModel;

        public UpdateArenaPresenter(ViewUpdater updater)
        {
            this._updater = updater;
        }

        public void Complete(Arena arena)
        {
            this._viewModel = new ViewModel();

            //マップ画像
            this.AddMapGraphs(arena);

            //選択マス
            this.AddSelectSquareGraph(arena);

            //ユニット情報欄
            this.AddUnitInfoElements(arena);

            //コマンド欄
            this.AddCommandPanelElements(arena);

            //ユニット移動可能領域
            this.AddMovableAreaGraphs(arena);

            //ユニット攻撃可能領域
            this.AddAttackableAreaGraphs(arena);

            //COMのターンの文字
            this.AddComTurnStartLabel(arena);

            //Playerのターンの文字
            this.AddPlayerTurnStartLabel(arena);

            _updater.CreateViewModel = _viewModel;
        }

        /// <summary>
        /// マップチップの画像をViewModelに追加
        /// </summary>
        /// <param name="arena"></param>
        private void AddMapGraphs(Arena arena)
        {
            int x, y, graphId;
            int width = Image.Constants.MAPCHIP_WIDTH;
            int height = Image.Constants.MAPCHIP_HEIGHT;

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
                    _viewModel.graphs.Add(graph);

                    if (arena.map[i][j].Unit is null)
                    {
                        continue;
                    }

                    //ユニットの描画
                    imageId = arena.map[i][j].Unit.ImageId;
                    graphId = Image.GetInstance().charachips[imageId];

                    Graph unitGraph = new Graph(graphId, x, y, width, height);
                    _viewModel.graphs.Add(unitGraph);
                }
            }

        }

        /// <summary>
        /// 選択中のマスを示す画像をViewModelに追加
        /// </summary>
        /// <param name="arena"></param>
        private void AddSelectSquareGraph(Arena arena)
        {
            //選択マスの描画
            int width = Image.Constants.MAPCHIP_WIDTH;
            int height = Image.Constants.MAPCHIP_HEIGHT;
            int graphId = Image.GetInstance().selectSign;
            int x = arena.cursorPoint.X * width;
            int y = arena.cursorPoint.Y * height;
            Graph selectGraph = new Graph(graphId, x, y, width, height);
            _viewModel.graphs.Add(selectGraph);
        }

        /// <summary>
        /// ユニット情報の画像とテキストをViewModelに追加
        /// </summary>
        /// <param name="arena"></param>
        private void AddUnitInfoElements(Arena arena)
        {
            Unit unit = arena.GetUnderCursorUnit();

            //ユニットがいる場合はユニット情報欄を表示
            if (!(unit is null))
            {
                int graphId = Image.GetInstance().frame;
                int x = Constants.UnitInfo.TOPLEFT_X;
                int y = Constants.UnitInfo.TOPLEFT_Y;
                int width = Constants.UnitInfo.WIDTH;
                int height = Constants.UnitInfo.HEIGHT;
                string content;
                byte transparency = Constants.UnitInfo.TRANSPARENCY;

                _viewModel.graphs.Add(new Graph(graphId, x, y, width, height, true, transparency));

                //Name
                x = Constants.UnitInfo.Name.X;
                y = Constants.UnitInfo.Name.Y;
                _viewModel.texts.Add(new Text(unit.Name, x, y, Constants.Color.ENABLED_COLOR));

                //HP
                content = Constants.UnitInfo.HP.LABEL + " " + unit.CurrentHp + "/" + unit.MaxHp;
                x = Constants.UnitInfo.HP.X;
                y = Constants.UnitInfo.HP.Y;
                _viewModel.texts.Add(new Text(content, x, y, Constants.Color.ENABLED_COLOR));

                //MP
                content = Constants.UnitInfo.MP.LABEL + " " + unit.CurrentMp + "/" + unit.MaxMp;
                x = Constants.UnitInfo.MP.X;
                y = Constants.UnitInfo.MP.Y;
                _viewModel.texts.Add(new Text(content, x, y, Constants.Color.ENABLED_COLOR));

                //AT
                content = Constants.UnitInfo.AT.LABEL + " " + unit.Attack;
                x = Constants.UnitInfo.AT.X;
                y = Constants.UnitInfo.AT.Y;
                _viewModel.texts.Add(new Text(content, x, y, Constants.Color.ENABLED_COLOR));

                //DF
                content = Constants.UnitInfo.DF.LABEL + " " + unit.Deffence;
                x = Constants.UnitInfo.DF.X;
                y = Constants.UnitInfo.DF.Y;
                _viewModel.texts.Add(new Text(content, x, y, Constants.Color.ENABLED_COLOR));

                //MAT
                content = Constants.UnitInfo.MAT.LABEL + " " + unit.MagicAttack;
                x = Constants.UnitInfo.MAT.X;
                y = Constants.UnitInfo.MAT.Y;
                _viewModel.texts.Add(new Text(content, x, y, Constants.Color.ENABLED_COLOR));

                //MDF
                content = Constants.UnitInfo.MDF.LABEL + " " + unit.MagicDeffence;
                x = Constants.UnitInfo.MDF.X;
                y = Constants.UnitInfo.MDF.Y;
                _viewModel.texts.Add(new Text(content, x, y, Constants.Color.ENABLED_COLOR));

                //TEC
                content = Constants.UnitInfo.TEC.LABEL + " " + unit.Technic;
                x = Constants.UnitInfo.TEC.X;
                y = Constants.UnitInfo.TEC.Y;
                _viewModel.texts.Add(new Text(content, x, y, Constants.Color.ENABLED_COLOR));

                //LUC
                content = Constants.UnitInfo.LUC.LABEL + " " + unit.Luck;
                x = Constants.UnitInfo.LUC.X;
                y = Constants.UnitInfo.LUC.Y;
                _viewModel.texts.Add(new Text(content, x, y, Constants.Color.ENABLED_COLOR));

            }

        }

        /// <summary>
        /// コマンド欄の画像とテキストをViewModelに追加
        /// </summary>
        /// <param name="arena"></param>
        private void AddCommandPanelElements(Arena arena)
        {
            if (arena.state is SelectCommandState)
            {
                string content;
                uint color;
                Unit unit = arena.selectedUnit;

                int graphId = Image.GetInstance().frame;
                int x = Constants.Command.TOPLEFT_X;
                int y = Constants.Command.TOPLEFT_Y;
                int width = Constants.Command.WIDTH;
                int height = Constants.Command.HEIGHT;
                byte transparency = Constants.Command.TRANSPARENCY;

                _viewModel.graphs.Add(new Graph(graphId, x, y, width, height, true, transparency));

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
                _viewModel.texts.Add(new Text(content, x, y, color));

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

                _viewModel.texts.Add(new Text(content, x, y, color));

                //ターン終了
                content = Constants.Command.TurnEnd.LABEL;
                x = Constants.Command.TurnEnd.X;
                y = Constants.Command.TurnEnd.Y;
                _viewModel.texts.Add(new Text(content, x, y, Constants.Color.ENABLED_COLOR));

                //選択
                content = Constants.Command.ALLOW;
                x = Constants.Command.TOPLEFT_X + Constants.Command.colX[0];
                y = Constants.Command.TOPLEFT_Y + Constants.Command.rowY[arena.commandPanel.selectedIndex];
                _viewModel.texts.Add(new Text(content, x, y, Constants.Color.ENABLED_COLOR));
            }

        }

        /// <summary>
        /// 移動可能なエリアを示す画像をViewModelに追加
        /// </summary>
        /// <param name="arena"></param>
        private void AddMovableAreaGraphs(Arena arena)
        {
            //移動範囲
            if (arena.movablePoints.Count > 0)
            {
                int width = Image.Constants.MAPCHIP_WIDTH;
                int height = Image.Constants.MAPCHIP_HEIGHT;
                foreach (Point point in arena.movablePoints)
                {
                    int x = point.X * width;
                    int y = point.Y * height;
                    Graph graph = new Graph(Image.GetInstance().movableSign, x, y, width, height, true);
                    _viewModel.graphs.Add(graph);
                }
            }
        }

        /// <summary>
        /// 攻撃可能なエリアを示す画像をViewModelに追加
        /// </summary>
        /// <param name="arena"></param>
        private void AddAttackableAreaGraphs(Arena arena)
        {
            if (arena.attackablePoints.Count > 0)
            {
                int width = Image.Constants.MAPCHIP_WIDTH;
                int height = Image.Constants.MAPCHIP_HEIGHT;
                foreach (Point point in arena.attackablePoints)
                {
                    int x = point.X * width;
                    int y = point.Y * height;
                    Graph graph = new Graph(Image.GetInstance().attackableSign, x, y, width, height, true);
                    _viewModel.graphs.Add(graph);
                }
            }
        }

        /// <summary>
        /// COMターン開始のテキストをViewModelに追加
        /// </summary>
        /// <param name="arena"></param>
        private void AddComTurnStartLabel(Arena arena)
        {
            if (arena.state is ComTurnStart)
            {
                string content = Constants.Turn.COM_TURN_START_TEXT;
                int x = Constants.Turn.TOPLEFT_X;
                int y = Constants.Turn.TOPLEFT_Y;
                uint color = Constants.Color.ENABLED_COLOR;
                int fontSize = Constants.Turn.FONT_SIZE;

                _viewModel.texts.Add(new Text(content, x, y, color, fontSize));
            }
        }

        /// <summary>
        /// プレイヤーターン開始のテキストをViewModelに追加
        /// </summary>
        /// <param name="arena"></param>
        private void AddPlayerTurnStartLabel(Arena arena)
        {
            int fontSize = Constants.Turn.FONT_SIZE;

            if (arena.state is ComTurnEnd)
            {
                string content = Constants.Turn.PLAYER_TURN_START_TEXT;
                int x = Constants.Turn.TOPLEFT_X;
                int y = Constants.Turn.TOPLEFT_Y;
                uint color = Constants.Color.ENABLED_COLOR;

                _viewModel.texts.Add(new Text(content, x, y, color, fontSize));
            }

        }

    }
}

using System;
using static DxLibDLL.DX;

namespace DxLibUI.Views
{
    internal class BattleView
        : IDisposable
    {

        private readonly ViewUpdater _viewUpdater;

        internal BattleView(ViewUpdater viewUpdater)
        {
            viewUpdater.Update += Update;
            _viewUpdater = viewUpdater;
        }

        public void Dispose()
        {
            _viewUpdater.Update -= Update;
        }


        private void Update(ViewModel viewModel)
        {
            foreach (Graph graph in viewModel.graphs)
            {
                //半透明設定なら半透明で描画
                if (graph.isTranslucent)
                {
                    SetDrawBlendMode(DX_BLENDMODE_ALPHA, 122);
                }

                DrawExtendGraph(graph.x, graph.y, graph.x + graph.width, graph.y + graph.height, graph.graphId, 1);

                //半透明設定ならもとの設定に戻す
                if (graph.isTranslucent)
                {
                    SetDrawBlendMode(DX_BLENDMODE_NOBLEND, 0);
                }
            }

            foreach (Text text in viewModel.texts)
            {
                DrawString(text.x, text.y, text.content, text.color);
            }

        }

    }
}

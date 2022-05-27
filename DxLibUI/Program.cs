using System;
using Microsoft.Extensions.DependencyInjection;
using DxLibUI.Controllers;
using DxLibUI.Views;
using static DxLibDLL.DX;

namespace DxLibUI
{
    internal class Program
    {

        static void Main(string[] args)
        {

            try
            {
                //ウィンドウモードで起動 0:フルスクリーン 1:ウィンドウ
                ChangeWindowMode(1);

                //ウィンドウサイズの変更
                SetGraphMode(Constants.Window.WIDTH, Constants.Window.HEIGHT, Constants.Window.COLOR_BIT_NUM);

                //ユーザにはウィンドウサイズの変更を許可しない
                SetWindowSizeChangeEnableFlag(FALSE, FALSE);

                //DxLibを初期化
                DxLib_Init();

                //DIコンテナを初期化
                StartUp.Run();
                var serviceCollection = StartUp.ServiceCollection;

                //ViewのUpdaterを初期化してDIコンテナに登録
                var updater = new ViewUpdater();
                serviceCollection.AddSingleton(updater);

                //Viewの初期化
                var serviceProvider = serviceCollection.BuildServiceProvider();
                var view = new BattleView(updater);

                //初期化用のコントローラ
                var initializeController = serviceProvider.GetService<InitializeController>();
                initializeController.Initialize(1);

                //毎フレーム描画を更新するためのコントローラ
                var updateArenaController = serviceProvider.GetService<UpdateArenaController>();

                //十字キーに対応するコントローラ
                //var UpController = serviceProvider.GetService<UpController>();
                //var DownController = serviceProvider.GetService<DownController>();
                //var LeftController = serviceProvider.GetService<LeftController>();
                //var RightController = serviceProvider.GetService<RightController>();

                //エンターキーに対応するコントローラ
                //var EnterController = serviceProvider.GetService<EnterController>();

                //エスケープに対応するコントローラ
                //var EscapeController = serviceProvider.GetService<EscapeController>();

                var userInputController = serviceProvider.GetService<UserInputController>();

                int[] keys = new int[256];

                //描画開始
                while (ProcessMessage() == 0)
                {
                    ClearDrawScreen();

                   //描画の更新
                    updateArenaController.UpdateArena();

                    GetHitKeyStateAllEx(keys);

                    //Escape
                    if (keys[KEY_INPUT_ESCAPE] == 1)
                    {
                        userInputController.Escape();
                        //EscapeController.Push();
                    } 
                    else if (keys[KEY_INPUT_ESCAPE] == 100)
                    {
                        break;
                    }

                    if (keys[KEY_INPUT_RETURN] == 1)
                    {
                        userInputController.Enter();
                        //EnterController.Push();
                    }

                    //W
                    if (keys[KEY_INPUT_W] == 1)
                    {
                        userInputController.Up();
                        //UpController.Push();
                    } 

                    //A
                    if (keys[KEY_INPUT_A] == 1)
                    {
                        userInputController.Left();
                        //LeftController.Push();
                    }

                    //S
                    if (keys[KEY_INPUT_S] == 1)
                    {
                        userInputController.Down();
                        //DownController.Push();
                    } 

                    //D
                    if (keys[KEY_INPUT_D] == 1)
                    {
                        userInputController.Right();
                        //RightController.Push();
                    }

 
                    ScreenFlip();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                DxLib_End();
            }

        }


    }
}

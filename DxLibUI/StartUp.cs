using Microsoft.Extensions.DependencyInjection;
using DxLibUI.Controllers;
using DxLibUI.Presenters;
using UseCase.Initialize;
using UseCase.Repositories;
using UseCase.UpdateArena;
using UseCase.UserInput;
using UseCase.Com;
using InMemoryInfrastructure;

namespace DxLibUI
{
    internal class StartUp
    {
        public static IServiceCollection ServiceCollection { get; } = new ServiceCollection();

        public static void Run()
        {
#if DEBUG
            SetUpDebug();
#else
            SetUpProduct();
#endif
        }

        public static void SetUpDebug()
        {
            //リポジトリ
            ServiceCollection.AddSingleton<IArenaRepository, InMemoryArenaRepository>();

            //画面描画関係の依存解決
            ServiceCollection.AddTransient<IUpdateArenaUseCase, UpdateArenaUseCase>();
            ServiceCollection.AddTransient<IUpdateArenaPresenter, UpdateArenaPresenter>();
            ServiceCollection.AddTransient<UpdateArenaController>();

            //画面生成関係の依存解決
            ServiceCollection.AddTransient<IInitializePresenter, InitializePresenter>();
            ServiceCollection.AddTransient<IInitializeUseCase, InitializeUseCase>();
            ServiceCollection.AddTransient<InitializeController>();

            //ユーザー入力のコントローラー
            ServiceCollection.AddTransient<UserInputController>();
            ServiceCollection.AddTransient<IUserInputUseCase, UserInputUseCase>();

            //コンピュータの思考
            //遷移関係


        }

        public static void SetUpProduct()
        {

        }



        
    }
}

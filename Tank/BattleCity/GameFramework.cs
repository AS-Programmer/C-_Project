using BattleCity.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleCity
{
    enum GameState
    {
        Running,GameOver
    }
    internal class GameFramework
    {
        // 设置一个全局的画布对象 canvas
        public static Graphics canvas;
        public static GameState gameState = GameState.Running;
        // 游戏开始时执行的方法
        public static void Start()
        {
            // 初始化音效
            SoundManager.InitSound();
            // 在游戏开始时调用 GameObjectManager 中的 CreateMap() 方法创建一次地图
            GameObjectManager.CreateMap();
            // 在游戏开始时调用 GameObjectManager 中的 CreateMyTank() 方法创建一次 myTank
            GameObjectManager.CreateMyTank();
            // 
            GameObjectManager.Start();

            // 调用游戏开始的声音
            SoundManager.PlayStart();
        }

        // 游戏进行时，频繁更新
        public static void Update()
        {
            // 在游戏进行时，每秒调用 GameObjectManager 中的 CreateMap() 方法绘制地图
            // 但是会地图闪烁，需要解决
            //GameObjectManager.DrawMap();
            // 在游戏进行时，每秒调用 GameObjectManager 中的 DrawMyTank() 方法绘制地图
            //GameObjectManager.DrawMyTank();
            
            if(gameState == GameState.Running)
            {
                GameObjectManager.Update();
            }
            else if(gameState == GameState.GameOver)
            {
                GameOverUpdate();
            }
        }

        private static void GameOverUpdate()
        {
            int x = 450 / 2 - Resources.GameOver.Width / 2;
            int y = 450 / 2 - Resources.GameOver.Height / 2;
            canvas.DrawImage(Resources.GameOver, x, y);
        }

        public static void GameOver()
        {
            gameState = GameState.GameOver;
        }
        
    }
}

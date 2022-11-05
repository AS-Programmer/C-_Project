using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleCity
{
    public partial class Form1 : Form
    {
        // 声明一个全局的线程对象
        private Thread thread;
        // 声明一个全局的静态的 windows 画布对象
        private static Graphics canvas;
        // 声明一个静态的 bitmap 图片对象
        private static Bitmap tempBitmap;
        public Form1()
        {
            InitializeComponent();
            // 设置窗口起始位置（屏幕中央）
            this.StartPosition = FormStartPosition.CenterScreen;

            // 获取当前窗体的画布
            canvas = this.CreateGraphics();
            // 处理墙壁闪烁问题，把每一帧需要画到的内容一次性全画到一个临时的画布上，然后再把临时画布画到窗体画布上
            // 创建一个和窗体一样大小的 bitmap 来一次性存储所有需要绘制的内容
            tempBitmap = new Bitmap(450, 450);
            // 创建一个 graphics 画布来绘制临时的图片内容，最后把画布绘制到窗体的画布 canvas 上
            Graphics graphics = Graphics.FromImage(tempBitmap);
            // 然后把这个已经画完的临时画布绘制到窗体上
            GameFramework.canvas = graphics;
            // 把画布给到 GameFramework 中的 canvas
            //GameFramework.canvas = canvas;

            // 声明线程
            thread = new Thread(new ThreadStart(GameMainThread));
            // 线程启动
            thread.Start();
        }

        // 游戏主要方法
        private static void GameMainThread()
        {
            // GameFramework

            GameFramework.Start();

            int sleepTime = 1000 / 60;

            // 60帧 看电脑配置
            while (true)
            {
                // 以某种颜色清空窗体背景
                GameFramework.canvas.Clear(Color.Black);
                // 每秒60帧
                GameFramework.Update();
                // 调用静态的 windows 画布对象把临时的图片对象 tempBitmap 画到窗体上
                canvas.DrawImage(tempBitmap, 0, 0); 
                // 每次调用完Update 暂停休息1/60秒
                Thread.Sleep(sleepTime);
            }
        }

        // 窗体关闭时的事件
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 强制关闭子线程
            thread.Abort();
        }

        // 键盘按下监听事件
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            GameObjectManager.KeyDown(e);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            GameObjectManager.KeyUp(e);
        }
    }
}

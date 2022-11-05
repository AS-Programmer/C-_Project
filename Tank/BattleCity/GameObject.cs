using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity
{
    // 声明为抽象类，则子类必须实现父类所有的抽象方法
    internal abstract class GameObject
    {
        // 通过属性直接生成x,y
        public int X { get; set; }
        public int Y { get; set; }
        // 设置物体高宽
        public int Width { get;set; }
        public int Height { get;set; }

        // 声明一个 protected 修饰的抽象方法获取到游戏对象的 image
        protected abstract Image GetImage();

        // 绘制自身的方法
        public virtual void DrawSelf()
        {
            // 直接访问到 GameFramework 中的 canvas画布
            Graphics canvas = GameFramework.canvas;
            // 绘制自身的图片
            canvas.DrawImage(GetImage(), X, Y);
        }

        public virtual void Update()
        {
            DrawSelf();
        }

        // 获取矩形的属性
        public Rectangle GetRectangle()
        {
            Rectangle rect = new Rectangle(X, Y, Width, Height);
            return rect;
        }
    }
}

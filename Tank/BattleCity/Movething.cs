using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity
{
    // 枚举类型四个朝向
    enum Direction
    {
        Up,Down,Left,Right
    }
    /// <summary>
    /// 可移动的游戏对象
    /// </summary>
    internal class Movething:GameObject
    {
        // 获取移动游戏对象的图片资源
        public Bitmap bitmapUp { get; set; }
        public Bitmap bitmapDown { get; set; }
        public Bitmap bitmapLeft { get; set; }
        public Bitmap bitmapRight { get; set; }

        // 速度
        public int speed { get; set; }
        // 游戏对象的朝向
        public Direction direction { get; set; }

        #region
        /*private Direction dir;
        // 游戏对象的朝向
        public Direction direction 
        { 
            get { return dir; }
            set
            {
                dir = value;
                Bitmap bmp  = null;
                switch (direction)
                {
                    case Direction.Up:
                        bmp = bitmapUp;
                        break;
                    case Direction.Down:
                        bmp = bitmapDown;
                        break;
                    case Direction.Left:
                        bmp = bitmapLeft;
                        break;
                    case Direction.Right:
                        bmp = bitmapRight;
                        break;
                }
                Width = bmp.Width;
                Height = bmp.Height;
            }
        }*/
        #endregion

        // 实现父类的抽象方法 GetImage()
        protected override Image GetImage()
        {
            Bitmap bitmap = null;
            // 根据枚举类的游戏对象的朝向返回游戏对象的朝向 image
            switch(direction)
            {
                case Direction.Up: 
                    bitmap = bitmapUp;
                    break;
                case Direction.Down:
                    bitmap = bitmapDown;
                    break;
                case Direction.Left:
                    bitmap = bitmapLeft;
                    break;
                case Direction.Right:
                    bitmap = bitmapRight;
                    break;
            }
            // 设置游戏对象的 image 的背景为黑色
            bitmap.MakeTransparent(Color.Black);
            Width = bitmap.Width;
            Height = bitmap.Height;
            return bitmap;
        }
    }
}

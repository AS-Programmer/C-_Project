using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity
{
    /// <summary>
    /// 不可移动的物体
    /// </summary>
    internal class NotMovething : GameObject
    {
        private Image img;
        public Image image 
        { 
            get 
            { 
                return img; 
            } 
            set 
            {
                img = value;
                Width = img.Width;
                Height = img.Height;
            }
        }

        // 实现父类的抽象方法 GetImage()
        protected override Image GetImage()
        {
            // 返回当前游戏对象的 image
            return image;
        }

        // NotMovething 的构造函数
        public NotMovething(int x, int y, Image img)
        {
            X = x;
            Y = y;
            image = img;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleCity.Properties;

namespace BattleCity
{
    internal class Explosion : GameObject
    {
        public bool isNeedDestroy { get; set; }
        private int playSpeed = 1;
        private int playCount = 0;
        private int index = 0;
        // 把爆炸图片放在一个数组中
        private Bitmap[] bmpArray = new Bitmap[]
        {
            Resources.EXP1,
            Resources.EXP2,
            Resources.EXP3,
            Resources.EXP4,
            Resources.EXP5
        };
        public Explosion(int x, int y)
        {
            foreach (Bitmap bmp in bmpArray)
            {
                bmp.MakeTransparent(Color.Black);
            }
            this.X = x - bmpArray[0].Width / 2;
            this.Y = y - bmpArray[0].Height / 2;
            isNeedDestroy = false;
        }

        // 通过 index 获取到要播放爆炸图片
        protected override Image GetImage()
        {
            if (index > 4) return bmpArray[4];
            return bmpArray[index];
        }

        // 每秒调用 playCount++ ，爆炸图片数组索引 index 设置为 (playCount - 1) / playSpeed
        public override void Update()
        {
            // 每秒计数器增加1
            playCount++;
            index = (playCount - 1) / playSpeed;
            if(index > 4)
            {
                isNeedDestroy = true;
            }
            base.Update();
        }

        public override void DrawSelf()
        {
            
            base.DrawSelf();
        }
    }
}

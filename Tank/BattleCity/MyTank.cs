using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BattleCity.Properties;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BattleCity
{
    internal class MyTank : Movething
    {
        public bool isMoving { get; set; }
        public int HP { get; set; }

        private int originalX;
        private int originalY;
        public MyTank(int x, int y, int speed)
        {
            isMoving = false;
            this.X = x;
            this.Y = y;
            originalX = x;
            originalY = y;
            this.speed = speed;
            bitmapDown = Resources.MyTankDown;
            bitmapUp = Resources.MyTankUp;
            bitmapLeft = Resources.MyTankLeft;
            bitmapRight = Resources.MyTankRight;
            // 默认朝上图片
            this.direction = Direction.Up;
            HP = 3;
        }

        private void Move()
        {
            if (isMoving)
            {
                switch (direction)
                {
                    case Direction.Up:
                        Y -= speed;
                        break;
                    case Direction.Down:
                        Y += speed;
                        break;
                    case Direction.Left:
                        X -= speed;
                        break;
                    case Direction.Right:
                        X += speed;
                        break;
                }
            }
            else
            {
                return;
            }
        }

        public override void Update()
        {
            // 移动检查
            MoveCheck();
            // 移动
            Move();
            // 调用父类方法绘制
            base.Update();
        }

        private void MoveCheck()
        {
            #region 检查有没有超过窗体边界
            if (direction == Direction.Up)
            {
                if (Y - speed < 0)
                {
                    isMoving = false;
                    return;
                }
            }
            else if (direction == Direction.Down)
            {
                if (Y + speed + Height > 450)
                {
                    isMoving = false;
                    return;
                }
            }
            else if (direction == Direction.Left)
            {
                if (X - speed < 0)
                {
                    isMoving = false;
                    return;
                }
            }
            else if (direction == Direction.Right)
            {
                if (X + speed + Width > 450)
                {
                    isMoving = false;
                    return;
                }
            }
            #endregion

            // 检查有没有和其他元素发生碰撞
            Rectangle rect = GetRectangle();
            switch (direction)
            {
                case Direction.Up:
                    rect.Y -= speed;
                    break;
                case Direction.Down:
                    rect.Y += speed;
                    break;
                case Direction.Left:
                    rect.X -= speed;
                    break;
                case Direction.Right:
                    rect.X += speed;
                    break;
            }
            // 判断碰撞的物体是否为空
            if (GameObjectManager.IsCollidedWall(rect) != null
                || GameObjectManager.IsCollidedSteel(rect) != null
                || GameObjectManager.IsCollidedBoss(rect))
            {
                // 如果碰撞到物体，取消移动
                isMoving = false;
                return;
            }
        }

        // 键盘按下事件监听
        public void KeyDown(KeyEventArgs args)
        {
            switch (args.KeyCode)
            {
                case Keys.W:
                    direction = Direction.Up;
                    isMoving = true;
                    break;
                case Keys.S:
                    direction = Direction.Down;
                    isMoving = true;
                    break;
                case Keys.A:
                    direction = Direction.Left;
                    isMoving = true;
                    break;
                case Keys.D:
                    direction = Direction.Right;
                    isMoving = true;
                    break;
                case Keys.Space:
                    // 发射子弹
                    Attack();
                    break;
            }
        }

        private void Attack()
        {
            SoundManager.PlayFire();
            int x = this.X;
            int y = this.Y;
            switch (direction)
            {
                case Direction.Up:
                    x += Width / 2;
                    break;
                case Direction.Down:
                    x += Width / 2;
                    y += Height;
                    break;
                case Direction.Left:
                    y += Height / 2;
                    break;
                case Direction.Right:
                    x += Width;
                    y += Height / 2;
                    break;
            }
            GameObjectManager.CreateBullet(x, y, direction, Tag.MyTank);
        }

        // 键盘松开事件监听
        public void KeyUp(KeyEventArgs args)
        {
            switch (args.KeyCode)
            {
                case Keys.W:
                    direction = Direction.Up;
                    isMoving = false;
                    break;
                case Keys.S:
                    direction = Direction.Down;
                    isMoving = false;
                    break;
                case Keys.A:
                    direction = Direction.Left;
                    isMoving = false;
                    break;
                case Keys.D:
                    direction = Direction.Right;
                    isMoving = false;
                    break;
            }
        }

        public void TakeDamage()
        {
            HP--;
            if(HP<= 0)
            {
                X = originalX;
                Y = originalY;
                HP = 4;
                direction = Direction.Up;
                SoundManager.PlayAdd();
            }
        }
    }
}

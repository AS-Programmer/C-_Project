using BattleCity.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity
{
    internal class EnemyTank : Movething
    {
        public int changeDirSpeed { get; set; }
        private int changeDirCount = 0;
        public int attackSpeed { get; set; }
        private int attackCount = 0;
        private Random random = new Random();
        public EnemyTank(int x, int y, int speed, Bitmap bmpDown, Bitmap bmpUp, Bitmap bmpLeft, Bitmap bmpRight)
        {
            this.X = x;
            this.Y = y;
            this.speed = speed;
            bitmapDown = bmpDown;
            bitmapUp = bmpUp;
            bitmapLeft = bmpLeft;
            bitmapRight = bmpRight;
            // 默认朝下图片
            this.direction = Direction.Down;
            attackSpeed = 60;
            changeDirSpeed = 70;
        }

        // 改变朝向
        private void ChangeDirection()
        {
            while (true)
            {
                Direction dir = (Direction)random.Next(0, 4);
                if(dir == direction)
                {
                    continue;
                }
                else
                {
                    direction = dir;
                    break;
                }
            }
            MoveCheck();
        }

        private void Move()
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

        public override void Update()
        {
            // 移动检查
            MoveCheck();
            // 移动
            Move();
            // 自动攻击
            AttackCheck();
            // AI移动
            AutoChangeDirection();
            // 调用父类方法绘制
            base.Update();
        }

        private void AutoChangeDirection()
        {
            changeDirCount++;
            if (changeDirCount < changeDirSpeed) return;
            ChangeDirection();
            changeDirCount = 0;
        }

        private void AttackCheck()
        {
            attackCount++;
            if (attackCount < attackSpeed) return;
            Attack();
            attackCount = 0;
        }

        private void MoveCheck()
        {
            #region 检查有没有超过窗体边界
            if (direction == Direction.Up)
            {
                if (Y - speed < 0)
                {
                    ChangeDirection();
                    return;
                }
            }
            else if (direction == Direction.Down)
            {
                if (Y + speed + Height > 450)
                {
                    ChangeDirection();
                    return;
                }
            }
            else if (direction == Direction.Left)
            {
                if (X - speed < 0)
                {
                    ChangeDirection();
                    return;
                }
            }
            else if (direction == Direction.Right)
            {
                if (X + speed + Width > 450)
                {
                    ChangeDirection();
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
                ChangeDirection();
                return;
            }
        }

        private void Attack()
        {
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
            GameObjectManager.CreateBullet(x, y, direction, Tag.EnemyTank);
        }

    }
}

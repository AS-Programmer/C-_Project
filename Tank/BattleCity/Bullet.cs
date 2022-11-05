using BattleCity.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity
{
    enum Tag
    {
        MyTank, EnemyTank
    }
    internal class Bullet : Movething
    {
        public Tag tag { get; set; }

        public Bullet(int x, int y, int speed,Direction dir,Tag tag)
        {
            this.X = x;
            this.Y = y;
            this.speed = speed;
            switch (dir)
            {
                case Direction.Up:
                    bitmapUp = Resources.BulletUp;
                    Width = bitmapUp.Width;
                    Height = bitmapUp.Height;
                    break;
                case Direction.Down:
                    bitmapDown = Resources.BulletDown;
                    Width = bitmapDown.Width;
                    Height = bitmapDown.Height;
                    break;
                case Direction.Left:
                    bitmapLeft = Resources.BulletLeft;
                    Width = bitmapLeft.Width;
                    Height = bitmapLeft.Height;
                    break;
                case Direction.Right:
                    bitmapRight = Resources.BulletRight;
                    Width = bitmapRight.Width;
                    Height = bitmapRight.Height;
                    break;
            }
            /*bitmapDown = Resources.BulletDown;
            bitmapUp = Resources.BulletUp;
            bitmapLeft = Resources.BulletLeft; ;
            bitmapRight = Resources.BulletRight;*/
            this.direction = dir;
            this.tag = tag;
            this.X -= Width / 2;
            this.Y -= Height / 2;
        }

        public override void DrawSelf()
        {
            base.DrawSelf();
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
            // 调用父类方法绘制
            base.Update();
        }

        private void MoveCheck()
        {
            #region 检查有没有超过窗体边界
            if (direction == Direction.Up)
            {
                if (Y + Height / 2 + 3 < 0)
                {
                    GameObjectManager.DestroyBullet(this);
                    return;
                }
            }
            else if (direction == Direction.Down)
            {
                if (Y + Height / 2 - 3 > 450)
                {
                    GameObjectManager.DestroyBullet(this);
                    return;
                }
            }
            else if (direction == Direction.Left)
            {
                if (X + Width / 2 + 3 < 0)
                {
                    GameObjectManager.DestroyBullet(this);
                    return;
                }
            }
            else if (direction == Direction.Right)
            {
                if (X + Width / 2 - 3 > 450)
                {
                    GameObjectManager.DestroyBullet(this);
                    return;
                }
            }
            #endregion

            // 检查有没有和其他元素发生碰撞
            Rectangle rect = GetRectangle();
            rect.X = X + Width / 2 - 3;
            rect.Y = Y + Height / 2 - 3;
            rect.Height = 6;
            rect.Width = 6;

            // 爆炸的中心点
            int xExplosion = this.X + Width / 2;
            int yExplosion = this.Y + Height / 2;
            // 判断碰撞的物体是否为空
            if (GameObjectManager.IsCollidedWall(rect) != null)
            {
                GameObjectManager.DestroyBullet(this);
                GameObjectManager.DestroyWall(GameObjectManager.IsCollidedWall(rect));
                GameObjectManager.CreateExplosion(xExplosion, yExplosion);
                SoundManager.PlayBlast();
                return;
            }
            if (GameObjectManager.IsCollidedSteel(rect) != null)
            {
                GameObjectManager.DestroyBullet(this);
                GameObjectManager.CreateExplosion(xExplosion, yExplosion);
                SoundManager.PlayBlast();
                return;
            }
            if (GameObjectManager.IsCollidedBoss(rect))
            {
                GameObjectManager.DestroyBullet(this);
                GameObjectManager.CreateExplosion(xExplosion, yExplosion);
                GameFramework.GameOver();
                SoundManager.PlayBlast();
                return;
            }
            if (tag == Tag.MyTank)
            {
                if(GameObjectManager.IsCollidedEnemyTank(rect) != null)
                {
                    GameObjectManager.DestroyBullet(this);
                    GameObjectManager.DestroyTank(GameObjectManager.IsCollidedEnemyTank(rect));
                    GameObjectManager.CreateExplosion(xExplosion, yExplosion);
                    //SoundManager.PlayHit();
                    SoundManager.PlayBlast();
                    return;
                }
            }
            else if(tag == Tag.EnemyTank)
            {
                MyTank myTank = null;
                if ((myTank=GameObjectManager.IsCollidedMyTank(rect)) != null)
                {
                    GameObjectManager.DestroyBullet(this);
                    myTank.TakeDamage();
                    GameObjectManager.CreateExplosion(xExplosion, yExplosion);
                    //SoundManager.PlayBlast();
                    SoundManager.PlayHit();
                    return;
                }
            }
        }
    }
}

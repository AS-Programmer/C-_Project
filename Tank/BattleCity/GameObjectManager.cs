using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BattleCity.Properties;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BattleCity
{
    internal class GameObjectManager
    {
        // 创建一个静态的红墙 wallList 用来存储墙，以便管理墙壁
        private static List<NotMovething> wallList = new List<NotMovething>();
        // 创建一个静态的钢铁墙 steelList 用来存储墙，以便管理墙壁
        private static List<NotMovething> steelList = new List<NotMovething>();
        // 创建一个静态的boss 用来存储boss图标，以便管理boss图标
        private static NotMovething boss;
        // 获取红墙对象
        private static Image imgWall = Resources.wall;
        // 获取钢铁墙对象
        private static Image imgSteel = Resources.steel;
        // 获取boss对象
        private static Image imgBoss = Resources.Boss;
        // 设置静态的 myTank
        private static MyTank myTank;
        // 敌人生成的速度
        private static int enemyBornSpeed = 60;
        // 敌人生成的计数器
        private static int enemyBornCount = 60;
        // 生成敌人的三个位置
        private static Point[] points = new Point[3];
        // 集合管理坦克
        private static List<EnemyTank> tankList = new List<EnemyTank>();
        // 集合管理子弹
        private static List<Bullet> bulletList = new List<Bullet>();
        // 爆炸图片集合管理
        private static List<Explosion> expList = new List<Explosion>();

        public static void Start()
        {
            points[0] = new Point(0, 0);
            points[1] = new Point(7 * 30, 0);
            points[2] = new Point(14 * 30, 0);
        }

        public static void Update()
        {
            // 遍历地图列表 wallList
            foreach (NotMovething temp in wallList)
            {
                // 调用父类中的 DrawSelf() 方法在窗体画布中绘制自身
                temp.Update();
            }

            // 遍历地图列表 steelList
            foreach (NotMovething temp in steelList)
            {
                // 调用父类中的 DrawSelf() 方法在窗体画布中绘制自身
                temp.Update();
            }
            // 调用父类方法DrawSelf() boss 
            boss.Update();

            // 调用父类方法DrawSelf() myTank
            myTank.Update();

            // 生成敌人
            enemyBorn();

            // 遍历敌人列表 tankList 生成敌人
            foreach (EnemyTank tank in tankList)
            {
                tank.Update();
            }
            // 遍历子弹列表 bulletList 生成子弹
            try
            {
                foreach (Bullet bullet in bulletList)
                {
                    // 调用父类中的 DrawSelf() 方法在窗体画布中绘制自身
                    bullet.Update();
                }
            }
            catch
            {

            }
            // 遍历爆炸图片列表 expList
            foreach (Explosion exp in expList)
            {
                // 调用父类中的 DrawSelf() 方法在窗体画布中绘制自身
                exp.Update();
            }
            CheckAndDestroyExplosion();
        }

        // 创建爆炸图片
        public static void CreateExplosion(int x,int y)
        {
            Explosion exp = new Explosion(x, y);
            expList.Add(exp);
        }

        // 检查是否需要销毁爆炸效果图片
        private static void CheckAndDestroyExplosion()
        {
            List<Explosion> needToDestroy = new List<Explosion>();
            foreach (Explosion exp in expList)
            {
                if(exp.isNeedDestroy == true)
                {
                    needToDestroy.Add(exp);
                }
            }
            foreach (Explosion exp in needToDestroy)
            {
                expList.Remove(exp);
            }
        }

        public static void CreateBullet(int x, int y, Direction dir, Tag tag)
        {

            Bullet bullet = new Bullet(x, y, 10, dir, tag);
            bulletList.Add(bullet);
        }

        public static void DestroyBullet(Bullet bullet)
        {
            bulletList.Remove(bullet);
        }

        public static void DestroyWall(NotMovething wall)
        {
            wallList.Remove(wall);
        }

        public static MyTank IsCollidedMyTank(Rectangle rect)
        {
            if (myTank.GetRectangle().IntersectsWith(rect))
            {
                return myTank;
            }
            else
            {
                return null;
            }
        }

        public static EnemyTank IsCollidedEnemyTank(Rectangle rect)
        {
            foreach(EnemyTank tank in tankList)
            {
                if (tank.GetRectangle().IntersectsWith(rect))
                {
                    return tank;
                }
            }
            return null;
        }

        public static void DestroyTank(EnemyTank tank)
        {
            tankList.Remove(tank);
        }

        private static void enemyBorn()
        {
            enemyBornCount++;
            if (enemyBornCount < enemyBornSpeed) return;
            // 
            Random rand = new Random();
            int index = rand.Next(0, 3);
            Point position = points[index];

            int enemyType = rand.Next(1, 5);
            switch (enemyType)
            {
                case 1:
                    CreateEnemyTank1(position.X, position.Y);
                    break;
                case 2:
                    CreateEnemyTank2(position.X, position.Y);
                    break;
                case 3:
                    CreateEnemyTank3(position.X, position.Y);
                    break;
                case 4:
                    CreateEnemyTank4(position.X, position.Y);
                    break;
            }

            enemyBornCount = 0;
        }

        private static void CreateEnemyTank1(int x, int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 2, Resources.GrayDown, Resources.GrayUp, Resources.GrayLeft, Resources.GrayRight);
            tankList.Add(tank);
        }

        private static void CreateEnemyTank2(int x, int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 2, Resources.GreenDown, Resources.GreenUp, Resources.GreenLeft, Resources.GreenRight);
            tankList.Add(tank);
        }

        private static void CreateEnemyTank3(int x, int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 4, Resources.QuickDown, Resources.QuickUp, Resources.QuickLeft, Resources.QuickRight);
            tankList.Add(tank);
        }

        private static void CreateEnemyTank4(int x, int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 1, Resources.SlowDown, Resources.SlowUp, Resources.SlowLeft, Resources.SlowRight);
            tankList.Add(tank);
        }

        // 判断是否和墙体发生碰撞
        public static NotMovething IsCollidedWall(Rectangle rect)
        {
            // 遍历墙体列表 wallList
            foreach (NotMovething wall in wallList)
            {
                // 如果墙体的矩形与当前物体矩形相交
                if (wall.GetRectangle().IntersectsWith(rect))
                {
                    // 返回相交的墙体
                    return wall;
                }
            }
            // 返回空，代表没与墙体发生碰撞
            return null;
        }

        // 判断是否与钢铁墙体发生碰撞
        public static NotMovething IsCollidedSteel(Rectangle rect)
        {
            // 遍历墙体列表 wallList
            foreach (NotMovething wall in steelList)
            {
                // 如果墙体的矩形与当前物体矩形相交
                if (wall.GetRectangle().IntersectsWith(rect))
                {
                    // 返回相交的墙体
                    return wall;
                }
            }
            // 返回空，代表没与墙体发生碰撞
            return null;
        }

        // 判断是否与Boss发生碰撞
        public static bool IsCollidedBoss(Rectangle rect)
        {
            // 返回碰撞的结果
            return boss.GetRectangle().IntersectsWith(rect);
        }

        #region 绘制地图
        /*public static void DrawMap()
        {
            // 遍历地图列表 wallList
            foreach (NotMovething temp in wallList)
            {
                // 调用父类中的 DrawSelf() 方法在窗体画布中绘制自身
                temp.DrawSelf();
            }

            // 遍历地图列表 steelList
            foreach (NotMovething temp in steelList)
            {
                // 调用父类中的 DrawSelf() 方法在窗体画布中绘制自身
                temp.DrawSelf();
            }
            // 调用父类方法DrawSelf() boss 
            boss.DrawSelf();
        }*/
        #endregion

        // 静态方法创建地图
        public static void CreateMap()
        {
            // 调用 CreateWall() 方法在(1,1)(30,30)的位置创建5个墙壁
            CreateWall(1, 1, 5, imgWall, wallList);
            CreateWall(3, 1, 5, imgWall, wallList);
            CreateWall(5, 1, 4, imgWall, wallList);
            CreateWall(7, 1, 3, imgWall, wallList);

            CreateWall(7, 5, 1, imgSteel, steelList);

            CreateWall(9, 1, 4, imgWall, wallList);
            CreateWall(11, 1, 5, imgWall, wallList);
            CreateWall(13, 1, 5, imgWall, wallList);

            CreateWall(0, 7, 1, imgSteel, steelList);

            CreateWall(2, 7, 1, imgWall, wallList);
            CreateWall(3, 7, 1, imgWall, wallList);
            CreateWall(4, 7, 1, imgWall, wallList);
            CreateWall(6, 7, 1, imgWall, wallList);
            CreateWall(7, 6, 2, imgWall, wallList);
            CreateWall(8, 7, 1, imgWall, wallList);
            CreateWall(10, 7, 1, imgWall, wallList);
            CreateWall(11, 7, 1, imgWall, wallList);
            CreateWall(12, 7, 1, imgWall, wallList);

            CreateWall(14, 7, 1, imgSteel, steelList);

            CreateWall(1, 9, 5, imgWall, wallList);
            CreateWall(3, 9, 5, imgWall, wallList);
            CreateWall(5, 9, 3, imgWall, wallList);
            CreateWall(6, 10, 1, imgWall, wallList);
            CreateWall(7, 10, 1, imgWall, wallList);
            CreateWall(8, 10, 1, imgWall, wallList);
            CreateWall(9, 9, 3, imgWall, wallList);
            CreateWall(11, 9, 5, imgWall, wallList);
            CreateWall(13, 9, 5, imgWall, wallList);

            CreateWall(6, 13, 2, imgWall, wallList);
            CreateWall(7, 13, 1, imgWall, wallList);
            CreateWall(8, 13, 2, imgWall, wallList);

            CreateBoss(7, 14, 1, imgBoss);
        }

        /*public static void DrawMyTank()
        {
            myTank.Update();
        }*/

        public static void CreateMyTank()
        {
            int x = 5 * 30;
            int y = 14 * 30;
            myTank = new MyTank(x, y, 2);
        }

        private static void CreateBoss(int x, int y, int count, Image image)
        {
            // 把x,y设置 × 30 变成坐标，在距离原点(30,30)的位置开始绘制墙
            int xPosition = x * 30;
            int yPosition = y * 30;
            boss = new NotMovething(xPosition, yPosition, imgBoss);
        }

        // 静态方法创建墙壁 x,y代表个数 需要设置返回值类型为 List<NotMovething>
        private static void CreateWall(int x, int y, int count, Image image, List<NotMovething> list)
        {
            // 把x,y设置 × 30 变成坐标，在距离原点(30,30)的位置开始绘制墙
            int xPosition = x * 30;
            int yPosition = y * 30;
            // 让 i 等于 yPosition，开始遍历 yPosition 坐标 + 5个墙 * 30的宽度 一个墙15像素
            for (int i = yPosition; i < yPosition + count * 30; i += 15)
            {
                // 墙的坐标(xPosition,i)   (xPosition + 15,i)
                NotMovething wall1 = new NotMovething(xPosition, i, image);
                NotMovething wall2 = new NotMovething(xPosition + 15, i, image);
                // 把需要绘制的墙体存储到 wallList 列表中，统一进行绘制
                list.Add(wall1);
                list.Add(wall2);
            }
        }

        // 键盘按下事件监听
        public static void KeyDown(KeyEventArgs args)
        {
            myTank.KeyDown(args);
        }

        // 键盘松开事件监听
        public static void KeyUp(KeyEventArgs args)
        {
            myTank.KeyUp(args);
        }
    }
}

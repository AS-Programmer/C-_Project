using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tank
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // 设置窗体生成的位置
            // CenterScreen：屏幕正中间  Manual：自己设置生成的位置
            this.StartPosition = FormStartPosition.CenterScreen;
            //this.StartPosition = FormStartPosition.Manual;
            // 生成一个点，以该点为起点绘制，一般以屏幕左上角为原点
            // Y轴正方向朝下
            //this.Location = new Point(850,550);
            
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // 构造一个颜色(a,r,g,b)
            //Color color = Color.FromArgb(255,255,0,0);
            // 创建一个 Graphics 对象 相当于画布
            Graphics canvas = this.CreateGraphics();

            #region 绘制线和字符串
            // 创建一个画笔对象(颜色)
            Pen pen = new Pen(Color.Black);
            // 窗体左上角为原点，Y轴朝下，X轴朝右
            // 画线(画笔，起始点，结束点)
            //canvas.DrawLine(pen, new Point(100,100), new Point(200,200));

            // 绘制字符串（字符串，字体（字体，大小），笔刷颜色，位置）
            //canvas.DrawString("Hello Tank！\n你好，坦克！",new Font("宋体",20),new SolidBrush(Color.Black),new Point(100,100));
            #endregion

            // 获取到 Properties.Resources 中的资源
            Image image = Properties.Resources.Boss;
            // Bitmap 继承 Image 
            Bitmap bitMap = Properties.Resources.Star1;
            // 让某个颜色变成透明的
            bitMap.MakeTransparent(Color.Black);
            // 绘制图片（图片资源，x，y）
            canvas.DrawImage(bitMap, 100, 200);
            // 绘制图片（图片资源，x，y）
            canvas.DrawImage(image,200,200);


        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}

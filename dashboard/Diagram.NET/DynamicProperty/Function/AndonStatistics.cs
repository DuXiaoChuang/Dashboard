using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections;

namespace Dalssoft.DiagramNet
{
    static public class AndonStatistics//绘制条形图
    {
        private static  int jianxi = 5;//间隙
        private static int tiaokuan = 12;//条形图的宽度
        private static int zikuan = 15;//字最小宽度
        private static float tukuan = 3;//每一个产品在图上的宽度
        private static Pen pen = new Pen(Color.Black, 1);//边框画笔
        private static SolidBrush wordBrush = new SolidBrush(Color.Black);//字体画刷

        static public void draw(Graphics g, Rectangle rec, List<int> data)
        {
            tukuan = 3;
            int start = rec.Left + 4;//左侧起始位置
            g.DrawLine(new Pen(Color.Black, 2), new Point(start-1, rec.Top), new Point(start-1, rec.Bottom));//左侧画一条基准线
            Rectangle rec2 = new Rectangle(start, rec.Top, rec.Width - zikuan, rec.Height);//条形图的精确位置，（去除文字宽度）
            Rectangle r1, r2, r3;//每一个竖条是一个矩形
            switch (data.Count)
            { 
                case 1:
                    while (tukuan * data[0] > rec2.Width)//长度太大则缩小
                    {
                        tukuan = tukuan / 2;
                    }
                    jianxi = (int)(rec2.Height - tiaokuan) / 2;
                    r1 = new Rectangle(rec2.X, (int)(rec2.Y + jianxi), (int)(tukuan * data[0]), tiaokuan);
                    g.FillRectangle(new SolidBrush(Color.Yellow), r1);
                    g.DrawRectangle(pen, r1);
                    g.DrawString(data[0].ToString(), new Font("宋体", tiaokuan, FontStyle.Bold), wordBrush, new PointF(r1.Right, r1.Top));
                    break;
                case 2:
                    while (tukuan * data[0] > rec2.Width || tukuan * data[1] > rec2.Width)//长度太大则缩小比例
                    {
                        tukuan = tukuan / 2;
                    }
                    jianxi = (int)((rec2.Height - tiaokuan * 2) / 3);
                    r1 = new Rectangle(rec2.X, rec2.Y + jianxi, (int)(tukuan * data[0]), tiaokuan);
                    g.FillRectangle(new SolidBrush(Color.Yellow), r1);
                    g.DrawRectangle(pen, r1);
                    g.DrawString(data[0].ToString(), new Font("宋体", tiaokuan, FontStyle.Bold), wordBrush, new PointF(r1.Right, r1.Top));

                    r2 = new Rectangle(rec2.X, r1.Bottom + jianxi, (int)(tukuan * data[1]), r1.Height);
                    g.FillRectangle(new SolidBrush(Color.Blue), r2);
                    g.DrawRectangle(pen, r2);
                    g.DrawString(data[1].ToString(), new Font("宋体", tiaokuan, FontStyle.Bold), wordBrush, new PointF(r2.Right, r2.Top));

                    break;
                case 3:
                    while (tukuan * data[0] > rec2.Width || tukuan * data[1] > rec2.Width || tukuan * data[2] > rec2.Width)//长度太大则缩小
                    {
                        tukuan = tukuan / 2;
                    }
                    jianxi = (int)((rec2.Height - tiaokuan * 3) / 4);
                    r1 = new Rectangle(rec2.X, rec2.Y + jianxi, (int)(tukuan * data[0]), tiaokuan);
                    g.FillRectangle(new SolidBrush(Color.Yellow), r1);
                    g.DrawRectangle(pen, r1);
                    g.DrawString(data[0].ToString(), new Font("宋体", tiaokuan, FontStyle.Bold), wordBrush, new PointF(r1.Right, r1.Top));

                    r2 = new Rectangle(rec2.X, r1.Bottom + jianxi, (int)(tukuan * data[1]), r1.Height);
                    g.FillRectangle(new SolidBrush(Color.Blue), r2);
                    g.DrawRectangle(pen, r2);
                    g.DrawString(data[1].ToString(), new Font("宋体", tiaokuan, FontStyle.Bold), wordBrush, new PointF(r2.Right, r2.Top));

                    r3 = new Rectangle(rec2.X, r2.Bottom + jianxi, (int)(tukuan * data[2]), r1.Height);
                    g.FillRectangle(new SolidBrush(Color.OrangeRed), r3);
                    g.DrawRectangle(pen, r3);
                    g.DrawString(data[2].ToString(), new Font("宋体", tiaokuan, FontStyle.Bold), wordBrush, new PointF(r3.Right, r3.Top));

                    break;
            }
        }
    }
}

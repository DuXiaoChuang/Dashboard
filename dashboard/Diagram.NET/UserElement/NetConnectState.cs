using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using Dalssoft.DiagramNet;
using System.Drawing.Design;
using System.ComponentModel;
using System.Data;

namespace Dalssoft.DiagramNet
{
    [
    Serializable,
    TypeConverter(typeof(ExpandableObjectConverter))
    ]

    public class NetConnectState : BaseElement, IControllable
    {
        [NonSerialized]
        private RectangleController controller;
        //protected Image imageWorking = Diagram.NET.Resource.connection;
        //protected Image imageAlert = Diagram.NET.Resource.nonconnection;
        protected LabelElement label = new LabelElement();
        protected string text = "";
        private direction Direction = direction.上下;
        private IsArrow isArrow = IsArrow.否;
        protected Color lineColor = Color.Green;
        protected int lineWidth = 1;
        protected int alarm = 0;
       




        [Category("监控信息")]
        [Description("监控对象")]
        [DynamicProps.DynamicList("SQL")]
        [TypeConverter(typeof(DynamicProps.NameConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public override string 监控对象
        {
            get
            {
                return monitorObject;
            }
            set
            {
                if (value != string.Empty)
                {
                    monitorObject = value;
                    DynamicProps.ListAttribute attributes = new DynamicProps.ListAttribute(SQL);
                    int index = attributes.codeNameCollection.IndexOf(value);
                    monitoredObjectID = attributes.idCollection[index].ToString();
                    monitoredObjectCode = attributes.codeCollection[index].ToString();
                    monitoredObjectName = attributes.nameCollection[index].ToString();
                    OnAppearanceChanged(new EventArgs());
                }
            }

        }
        [Category("样式")]
        [Description("线条宽度")]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(1)]
        public virtual int LineWidth
        {
            get
            {
                return lineWidth;
            }
            set
            {
                lineWidth = value;
                OnAppearanceChanged(new EventArgs());
            }
        }
        [Category("样式")]
        [Description("是否有箭头")]
        [RefreshProperties(RefreshProperties.All)]
        public virtual IsArrow 有无箭头
        {
            get
            {
                return isArrow;
            }
            set
            {
                isArrow = value;
                OnAppearanceChanged(new EventArgs());
            }
        }
        [Category("样式")]
        [Description("箭头方向")]
        [RefreshProperties(RefreshProperties.All)]
        public virtual direction 箭头方向
        {
            get
            {
                return Direction;
            }
            set
            {
                Direction = value;
            }

        }


        public NetConnectState()
            : this(0, 0, 100, 100)
        { }

        public NetConnectState(Rectangle rec)
            : this(rec.Location, rec.Size)
        { elementMonitoredType = MonitoredType.设备网络状态; }

        public NetConnectState(Point l, Size s)
            : this(l.X, l.Y, s.Width, s.Height)
        { }

        public NetConnectState(int top, int left, int width, int height)
        {
            location = new Point(top, left);
            size = new Size(width, height);
        }


        internal override void Draw(Graphics g)
        {
            Pen p1 = new Pen(Color.Green, lineWidth);
            p1.StartCap = LineCap.ArrowAnchor;
            p1.EndCap = LineCap.NoAnchor;

            Pen p2 = new Pen(Color.Green, lineWidth);
            p2.StartCap = LineCap.NoAnchor;
            p2.EndCap = LineCap.ArrowAnchor;

            Pen p3 = new Pen(Color.Red, lineWidth);
            p3.StartCap = LineCap.ArrowAnchor;
            p3.EndCap = LineCap.NoAnchor;


            Pen p4 = new Pen(Color.Red, lineWidth);
            p4.StartCap = LineCap.NoAnchor;
            p4.EndCap = LineCap.ArrowAnchor;


            Pen p5 = new Pen(Color.Green, lineWidth);
            p5.StartCap = LineCap.NoAnchor;
            p5.EndCap = LineCap.NoAnchor;

            Pen p6 = new Pen(Color.Red, lineWidth);
            p6.StartCap = LineCap.NoAnchor;
            p6.EndCap = LineCap.NoAnchor;

            IsInvalidated = false;
            Rectangle r = GetUnsignedRectangle(
                new Rectangle(
                location.X, location.Y,
                size.Width, size.Height));

            string sql = "select distinct connection_S from AT_P_IP where equip_key_I = '" + monitoredObjectID + "'";
            DataTable dt = data.DBQuery.OpenTable1(sql);
            if (dt.Rows.Count > 0)
            {
                text = dt.Rows[0][0].ToString().Trim();
            }
            else
            {
                text = "";
            }
            switch (text.ToString())
            {
                case "是":
                    {
                        if (isArrow == IsArrow.否)
                        {
                            if (Direction == direction.上下)
                            {
                                g.DrawLine(p5, location.X + size.Width / 2, location.Y, location.X + size.Width / 2, location.Y + size.Height);
                            }
                            else if (Direction == direction.下上)
                            {
                                g.DrawLine(p5, location.X + size.Width / 2, location.Y, location.X + size.Width / 2, location.Y + size.Height);
                            }
                            else if (Direction == direction.左右)
                            {
                                g.DrawLine(p5, location.X, location.Y + size.Height / 2, location.X + size.Width, location.Y + size.Height / 2);

                            }
                            else if (Direction == direction.右左)
                            {
                                g.DrawLine(p5, location.X, location.Y + size.Height / 2, location.X + size.Width, location.Y + size.Height / 2);

                            }
                        }
                        else
                        {
                            if (Direction == direction.上下)
                            {
                                g.DrawLine(p1, location.X + size.Width / 2, location.Y, location.X + size.Width / 2, location.Y + size.Height);
                            }
                            else if (Direction == direction.下上)
                            {
                                g.DrawLine(p2, location.X + size.Width / 2, location.Y, location.X + size.Width / 2, location.Y + size.Height);
                            }
                            else if (Direction == direction.左右)
                            {
                                g.DrawLine(p2, location.X, location.Y + size.Height / 2, location.X + size.Width, location.Y + size.Height / 2);

                            }
                            else if (Direction == direction.右左)
                            {
                                g.DrawLine(p1, location.X, location.Y + size.Height / 2, location.X + size.Width, location.Y + size.Height / 2);

                            }
                        }


                    }
                    break;
                case "否":
                    {
                        if (isArrow == IsArrow.否)
                        {
                            if (alarm == 0)
                            {
                                if (Direction == direction.上下)
                                {
                                    g.DrawLine(p6, location.X + size.Width / 2, location.Y, location.X + size.Width / 2, location.Y + size.Height);
                                }
                                else if (Direction == direction.下上)
                                {
                                    g.DrawLine(p6, location.X + size.Width / 2, location.Y, location.X + size.Width / 2, location.Y + size.Height);
                                }
                                else if (Direction == direction.左右)
                                {
                                    g.DrawLine(p6, location.X, location.Y + size.Height / 2, location.X + size.Width, location.Y + size.Height / 2);
                                }
                                else if (Direction == direction.右左)
                                {
                                    g.DrawLine(p6, location.X, location.Y + size.Height / 2, location.X + size.Width, location.Y + size.Height / 2);
                                }
                                alarm = 1;
                            }
                            else
                            {
                                if (Direction == direction.上下)
                                {
                                    g.DrawLine(p5, location.X + size.Width / 2, location.Y, location.X + size.Width / 2, location.Y + size.Height);
                                }
                                else if (Direction == direction.下上)
                                {
                                    g.DrawLine(p5, location.X + size.Width / 2, location.Y, location.X + size.Width / 2, location.Y + size.Height);
                                }
                                else if (Direction == direction.左右)
                                {
                                    g.DrawLine(p5, location.X, location.Y + size.Height / 2, location.X + size.Width, location.Y + size.Height / 2);

                                }
                                else if (Direction == direction.右左)
                                {
                                    g.DrawLine(p5, location.X, location.Y + size.Height / 2, location.X + size.Width, location.Y + size.Height / 2);

                                }
                                alarm = 0;

                            }


                        }
                        else
                        {
                            if (alarm == 0)
                            {
                                if (Direction == direction.上下)
                                {
                                    g.DrawLine(p3, location.X + size.Width / 2, location.Y, location.X + size.Width / 2, location.Y + size.Height);
                                }
                                else if (Direction == direction.下上)
                                {
                                    g.DrawLine(p4, location.X + size.Width / 2, location.Y, location.X + size.Width / 2, location.Y + size.Height);
                                }
                                else if (Direction == direction.左右)
                                {
                                    g.DrawLine(p4, location.X, location.Y + size.Height / 2, location.X + size.Width, location.Y + size.Height / 2);
                                }
                                else if (Direction == direction.右左)
                                {
                                    g.DrawLine(p3, location.X, location.Y + size.Height / 2, location.X + size.Width, location.Y + size.Height / 2);
                                }
                                alarm = 1;
                            }
                            else
                            {
                                if (Direction == direction.上下)
                                {
                                    g.DrawLine(p1, location.X + size.Width / 2, location.Y, location.X + size.Width / 2, location.Y + size.Height);
                                }
                                else if (Direction == direction.下上)
                                {
                                    g.DrawLine(p2, location.X + size.Width / 2, location.Y, location.X + size.Width / 2, location.Y + size.Height);
                                }
                                else if (Direction == direction.左右)
                                {
                                    g.DrawLine(p2, location.X, location.Y + size.Height / 2, location.X + size.Width, location.Y + size.Height / 2);

                                }
                                else if (Direction == direction.右左)
                                {
                                    g.DrawLine(p1, location.X, location.Y + size.Height / 2, location.X + size.Width, location.Y + size.Height / 2);

                                }
                                alarm = 0;

                            }
                        }


                    }
                    break;


            }
        }



        #region interface 接口
        IController IControllable.GetController()
        {
            if (controller == null)
                controller = new RectangleController(this);
            return controller;
        }
        #endregion
    }
}


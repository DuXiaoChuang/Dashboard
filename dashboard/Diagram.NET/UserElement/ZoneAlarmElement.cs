using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Collections.Generic;

namespace Dalssoft.DiagramNet
{
    [Serializable]
    public class ZoneAlarmElement : BaseElement, IControllable
    {
        [NonSerialized]
        private RectangleController controller;
        protected EquipmentElement equipElement = new EquipmentElement();
        protected CommentBoxElement equipComment = new CommentBoxElement();
        protected RackElement rackElement = new RackElement();
        protected CommentBoxElement rackComment = new CommentBoxElement();
        protected CommentBoxElement ZoneComment = new CommentBoxElement();




        
        [RefreshProperties(RefreshProperties.All)]
        public virtual EquipmentElement 设备
        {
            get
            {
                return equipElement;
            }
            set
            {
                equipElement = value;
                OnAppearanceChanged(new EventArgs());
            }
        }
        public virtual CommentBoxElement 设备文本
        {
            get
            {
                return equipComment;
            }
            set
            {
                equipComment = value;
                OnAppearanceChanged(new EventArgs());
            }
        }

        public virtual RackElement 物料
        {
            get
            {
                return rackElement;
            }
            set
            {
                rackElement = value;
                OnAppearanceChanged(new EventArgs());
            }
        }
        public virtual CommentBoxElement 物料文本
        {
            get
            {
                return rackComment;
            }
            set
            {
                rackComment = value;
                OnAppearanceChanged(new EventArgs());
            }
        }
        public virtual CommentBoxElement 区域文本
        {
            get
            {
                return ZoneComment;
            }
            set
            {
                ZoneComment = value;
                OnAppearanceChanged(new EventArgs());
            }
        }

        public ZoneAlarmElement(): this(0, 0, 100, 100)
        { }

        public ZoneAlarmElement(Rectangle rec): this(rec.Location, rec.Size)
        {
            elementMonitoredType = MonitoredType.区域报警;
            ZoneComment = new CommentBoxElement(this.Location.X + 40, this.Location.Y + 1, 40, 20);
            equipElement = new EquipmentElement(this.Location.X + 1, this.Location.Y + 20, 20, 20);
            equipComment = new CommentBoxElement(this.Location.X + 21, this.Location.Y + 1, 40, 20);
            rackElement = new RackElement(this.Location.X + 1, this.Location.Y + 40, 20, 20);
            rackComment = new CommentBoxElement(this.Location.X + 40, this.Location.Y + 40, 40, 20);
        }

        public ZoneAlarmElement(Point l, Size s): this(l.X, l.Y, s.Width, s.Height)
        { }

        public ZoneAlarmElement(int top, int left, int width, int height)
        {
            location = new Point(top, left);
            size = new Size(width, height);
        }

        internal override void Draw(Graphics g)
        {
            IsInvalidated = false;

            Rectangle r = GetUnsignedRectangle(
                new Rectangle(
                location.X, location.Y,
                size.Width, size.Height));
            #region ANDON统计

            equipElement.Draw(g);
            equipComment.Draw(g);
            rackComment.Draw(g);
            rackElement.Draw(g);
            ZoneComment.Draw(g);

            #endregion
        }

        internal override void DrawAlert(Graphics g)
        {
            //IsInvalidated = false;

            //Rectangle r = GetUnsignedRectangle(
            //    new Rectangle(
            //    location.X, location.Y,
            //    size.Width, size.Height));
            //#region ANDON统计

            //List<int> data = new List<int>();
            //for (int i = 0; i < 3; i++)
            //{
            //    Random rand = new Random(Guid.NewGuid().GetHashCode());
            //    int RandKey = rand.Next(0, 20);
            //    data.Add(RandKey);
            //}
            //AndonStatistics.draw(g, r, data);

            //#endregion
        }

        public static void TextAutoSize(LabelElement lbl, BaseElement el)
        {
       

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

using System;
using System.Collections.Generic;
using System.Text;

namespace Dalssoft.DiagramNet
{

    public enum MonitoredType
    {

        无 = 0,
        设备 = 1,
        RFID = 2,
        ANDON = 3,
        ANDON统计 = 4,
        料架 = 5,
        人员 = 6,
        区域 = 7,
        控制地址 = 8,
        发动机 = 9,
        消息框 = 10,
        起始点 = 11,
        工控机 = 12,
        区域报警 = 13,
        工位 = 14,
        星期 = 16,
        TextTime=17,
        TextDate = 18,
        生产计划=19,
        andon控件 = 20,
        结构一股生产计划=21,
        设备网络状态=22,
        TextDynamic = 23,
        生产计划表头=24,
        StraightLine = 25,
        时间=26,
        工位模型=27,
        股=28,
        LED股名=29,
        料架1=30,
        结构二股生产计划 = 31,
        结构三股生产计划 = 32,
        配重股=33,
        金加工股=34,
        岗位质量ANDON=35,
        岗位物料ANDON=36,
        设备状态 = 37,
        设备报警信息表 = 38,
        刀具状态信息表 = 39,
        统计数量 = 40,
        产品跟踪表=41,
        LED生产情况=42,
        刀具状态=43,
        刀槽=44,
        温度=45,
        湿度=46,
        电能=47,
    };

    public enum Statistics_type
    {
        无 = 0,
        上线数量 = 1,
        下线数量 = 2,
        线上数量 = 3,
    }
    public enum Statistics_type1
    {
        无 = 0,
        区域1 = 1,
        区域2 = 2,
        区域3 = 3,
    }
    public enum CanvasType
    {
        无 = 0,
        区域监控 = 1,
        LED看板=2,
    };
    public enum GradientMode
    {
        水平 = 0,
        垂直 = 1,
        角部辐射 = 2,
        中心辐射 = 3,
    };
    public enum FillMode
    {
        单色 = 0,
        双色 = 1,
        //图片=2
    }
    public enum TextShownMode
    {
        无 = 0,
        名称 = 1,
        编号 = 2,
    }
    public enum TypeOfDesigner
    {
        配置 = 0,
        监控 = 1,
    }
    public enum IsShowGrid
    {
        否 = 0,
        是 = 1,
    }
    public enum IsArrow
    {
        否 = 0,
        是 = 1,
    }
    public enum ObjectState
    {
        无 = 0,
        报警 = 1,
        工作中 = 2,
    }
    public enum ObjectState2
    {
        image1不亮image2不亮 = 0,
        image1亮image2不亮 = 1,
        image1不亮image2亮 = 2,
        image1亮image2亮 = 3,
    }
    public enum MovementDirection
    {
        向右 = 0,
        向左 = 1,
        向上 = 2,
        向下 = 3,
        无=4,
    }
    public enum equip_style
    {
        单台设备 = 0,
        区域设备 = 1,
    }
    public enum gu_name
    {
        
        油漆股 = 0,
        结构三股 = 1,
        金加工股 = 2,
        结构二股 = 3,
        配重股= 4,
        下料股=5,
        成型股=6,
        结构一股=7,
        无=8,

    }
    public enum andon
    {

        质量 = 0,
        物料 = 1,
        无 = 3,

    }
    public enum Rack_style
    {
        单个料架 = 0,
        区域物料信息 = 1,
    }
    public enum direction
    {
        左右 = 0,
        右左 =1,
        上下 = 2,
        下上=3,
    }
    public enum movedirection
    {
        向右 = 0,
        向左 = 1,
    }

    public enum ProductionLineType
    {
        无 = 0,
        总装线 = 1,
        测试线 = 2,
        轴系分装线A = 3,
        轴系分装线B = 4,
        主壳分装线 = 5,
        HCU分装线 = 6,
        主壳体机加线 = 7,
        离壳体机加线 = 8
    }

    public enum ClockStyle
    {
        MilliSecond,//毫秒
        Second,//秒
        Minute,//分钟
        Hour,//小时

        Second_MilliSecond,//秒-毫秒
        Minute_Second,//分钟-秒
        Hour_Minute,//小时-分钟

        Minute_Second_MilliSecond,//分钟-秒-毫秒
        Hour_Minute_Second,//小时-分钟-秒

        Hour_Minute_Second_MilliSecond//小时-分钟-秒-毫秒
    }
}

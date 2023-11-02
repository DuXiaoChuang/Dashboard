using System;

namespace Dalssoft.DiagramNet
{
	internal enum CornerPosition: int
	{
		Nothing = -1,
		BottomCenter = 0,
		BottomLeft = 1,
		BottomRight = 2,
		MiddleCenter = 3,
		MiddleLeft = 4,
		MiddleRight = 5,
		TopCenter = 6,
		TopLeft = 7,
		TopRight = 8,
		Undefined = 99
	}

	public enum CardinalDirection
	{
		Nothing,
		North,
		South,
		East,
		West
	}

	public enum Orientation
	{
		Horizontal,
		Vertical
	}

    public enum ElementType
    {
        Rectangle,
        RectangleNode,
        Elipse,
        ElipseNode,
        CommentBox,
        Personnel,
        AndonStatis,
        EquipmentElement,
        IPC,
        Rfid,
        Andon,
        Rack,
        Zone,
        Address,
        Engine,
        StartEndPoint,
        zoneAlarmElement,
        Workcenter,
        TextWeek,
        TextTime,
        TextDate,
        WorkOrder,
        WorkOrderTile,
        LEDAndon,
        NetConnectState,
        StraightLine,
        DigitalTime,
        WorkCenterModel,
        gu,
        xiaoxi,
        Rack1,
        WorkAndon,
        zhiliang,
        wuliao,
        EquipState,
        EquipAlarmExcel,
        ToolStateExcel,
        OnlineNumber,
        ProductTraceExcel,
        ToolState,
        Groove,
        Temperature,
        shidu,
        dianneng,
    }

	public enum LinkType
	{
        Straight,//Ö±Ïß
		RightAngle//Ö±½Ç
	}

	internal enum LabelEditDirection
	{
		UpDown,
		Both
	}

}

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Dalssoft.DiagramNet
{
    public class ClassLib
    {
        public static ArrayList IPAddressList = new ArrayList();

        //public static void GetAllIPAddress(Document document)
        //{
        //    IPAddressList = new ArrayList();
        //    for (int i = 0; i < document.Elements.Count; i++)
        //    {
        //        BaseElement el = document.Elements[i];
        //        if (el.监控类型 == MonitoredType.设备)
        //        {
        //            EquipmentElement bl = (EquipmentElement)el;
        //            if (bl.IP地址 != string.Empty)
        //            {
        //                IPAddressList.Add(bl.IP地址);
        //            }
        //        }
        //    }

        //}
        public static bool IsIPAddressExisted(string ipAddress)
        {
            //if(IPAddressList.Count==0) return false;
            if (IPAddressList == null) return false;
            if (IPAddressList.Contains(ipAddress))
                return true;            
            else
                return false;
        }

    }
}

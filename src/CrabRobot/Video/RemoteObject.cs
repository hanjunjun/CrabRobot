using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using AForge.Controls;
using AForge.Video;
using AForge.Video.DirectShow;

namespace CrabRobot.Video
{
    /// <summary>
    /// 
    /// </summary>
    public class RemoteObject : MarshalByRefObject
    {
        public RemoteObject()
        {
        }
        public string Test()
        {
            return "录像服务";
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HBNiuBi.Video
{
    /// <summary>
    /// 
    /// </summary>
    public class IpcBootstrapService
    {
        #region Fields

        #endregion

        #region Ctor
        public IpcBootstrapService()
        {
        }
        #endregion

        #region Properties

        #endregion

        #region Methods
        public void StartServer()
        {
            //var dict = new Dictionary<string, string>();
            //dict["name"] = dict["portName"] = "ServerChannel";
            //dict["authorizedGroup"] = "Everyone";
            ////Instantiate our server channel.
            //IpcServerChannel channel = new IpcServerChannel(dict,null);
            ////Register the server channel.
            //ChannelServices.RegisterChannel(channel, false);
            ////Register this service type.
            //RemotingConfiguration.RegisterWellKnownServiceType(typeof(RemoteObject), "RemoteObject", WellKnownObjectMode.SingleCall);
            //Logger.Info("ipc server start！");
        }
        #endregion
    }
}

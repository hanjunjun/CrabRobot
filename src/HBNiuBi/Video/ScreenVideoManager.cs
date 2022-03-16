using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.FFMPEG;

namespace HBNiuBi.Video
{
    /// <summary>
    /// 
    /// </summary>
    public class ScreenVideoManager
    {
        #region Fields
        public bool Running;
        private List<string> _screenNames;
        //private Rectangle _screenSize;
        private uint _frameCount;
        private VideoFileWriter _writer;
        private int _width;
        private int _height;
        private ScreenCaptureStream _streamVideo;
        private Rectangle _screenArea;
        private int fps;
        private VideoCodec codec;
        private BitRate bitRate;
        private int screen;
        private string path;
        #endregion

        #region Ctor
        public ScreenVideoManager( string path, int fps = 10, int screen = 0, VideoCodec codec = VideoCodec.FLV1, BitRate bitRate = BitRate._1000kbit)
        {
            Running = false;
            //this._screenSize = Screen.PrimaryScreen.Bounds;
            _frameCount = 0;
            _width = SystemInformation.VirtualScreen.Width;
            _height = SystemInformation.VirtualScreen.Height;
            _screenArea = Rectangle.Empty;
            _writer = new VideoFileWriter();
            this.fps = fps;
            this.codec = codec;
            this.bitRate = bitRate;
            this.screen = screen;
            this.path = path;
            _screenNames = new List<string>();
        }
        #endregion

        #region Properties
        public enum BitRate
        {
            _50kbit = 5000,
            _100kbit = 10000,
            _500kbit = 50000,
            _1000kbit = 1000000,
            _2000kbit = 2000000,
            _3000kbit = 3000000
        }
        #endregion

        #region Methods
        public void ExitScreenVideo(bool visible)
        {
            Running = visible;
            Thread.Sleep(2000);
        }
        public void StartRec()
        {
            if (Running == false)
            {
                SetScreenArea();

                ExitScreenVideo(true);

                _frameCount = 0;
                if (!Directory.Exists(Path.GetDirectoryName(path)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                }
                // Save File option
                _writer.Open(
                    path,
                    _width,
                    _height,
                    fps,
                    codec,
                    (int)bitRate);

                // Start main work
                StartRecord();
            }
        }

        private void SetScreenArea()
        {
            _screenArea = Screen.PrimaryScreen.Bounds;
            _width = _screenArea.Width;
            _height = _screenArea.Height;

            // get entire desktop area size
            //string screenName = Screen.AllScreens[this.screen].DeviceName;
            //if (string.Compare(screenName, @"Select ALL", StringComparison.OrdinalIgnoreCase) == 0)
            //{
            //    foreach (Screen screen in Screen.AllScreens)
            //    {
            //        this._screenArea = Rectangle.Union(_screenArea, screen.Bounds);
            //    }
            //}
            //else
            //{
            //    this._screenArea = Screen.AllScreens.First(scr => scr.DeviceName.Equals(screenName)).Bounds;
            //    this._width = this._screenArea.Width;
            //    this._height = this._screenArea.Height;
            //}
        }

        private void StartRecord() //Object stateInfo
        {
            // create screen capture video source
            _streamVideo = new ScreenCaptureStream(_screenArea);

            // set NewFrame event handler
            _streamVideo.NewFrame += new NewFrameEventHandler(video_NewFrame);
            // start the video source
            _streamVideo.Start();
        }
        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (Running)
            {
                DrawTimeInImage(eventArgs.Frame);
                _writer.WriteVideoFrame(eventArgs.Frame);
            }
            else
            {
                _streamVideo.SignalToStop();
                Thread.Sleep(500);
                _writer.Close();
            }
        }
        public static void DrawTimeInImage(Image image)
        {
            using (var graphics = Graphics.FromImage(image))
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                Font font = new Font("微软雅黑", 35, FontStyle.Bold);
                SolidBrush sbrush = new SolidBrush(Color.Red);
                graphics.DrawString(str, font, sbrush, new PointF(10, 10));
            }
        }
        #endregion
    }
}

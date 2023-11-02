using System;
using System.Collections.Generic;
using System.Text;
using WMPLib;

namespace HFUTIEMES
{
    public  class ClassMediaPlay
    {
        public string fileName = "";
        WindowsMediaPlayerClass player = new WindowsMediaPlayerClass();
        public ClassMediaPlay()
        { }
        public ClassMediaPlay(string _fileName)
        {
            fileName = _fileName;
        }
        public void PlayMedia()
        {
            if (player.playState == WMPPlayState.wmppsPlaying)     //playState:integer; 播放状态，1=停止，2=暂停，3=播放，6=正在缓冲，9=正在连接，10=准备就绪 
            {
                player.stop();
            }
            player.URL = fileName;//URL: String;指定媒体位置，本机或网络地址
            player.uiMode = "Invisible";//uiMode:String; 播放器界面模式，可为Full, Mini, None, Invisible
            //enableContextMenu:Boolean; 启用/禁用右键菜单 
            //fullScreen:boolean; 是否全屏显示  
            player.settings.volume = 100;
            player.settings.playCount = 1;
            player.settings.setMode("loop", true);
            player.play();

        }
        public void Stop()
        {
            if (player.playState == WMPPlayState.wmppsPlaying)     //playState:integer; 播放状态，1=停止，2=暂停，3=播放，6=正在缓冲，9=正在连接，10=准备就绪 
            {
                player.stop();
            }
            
        }

    }
}

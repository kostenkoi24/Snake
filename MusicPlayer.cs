using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    class MusicPlayer
    {
        static WMPLib.WindowsMediaPlayer Player;

        public static void PlayMusic()
        {
            Player = new WMPLib.WindowsMediaPlayer();
            Player.URL = ".\\Mp3\\boss-133663.mp3";
            Player.controls.play();
            Player.settings.setMode("loop", true);
        }

        public static void StopMusic()
        {
            Player.controls.stop();
        }




    }
}

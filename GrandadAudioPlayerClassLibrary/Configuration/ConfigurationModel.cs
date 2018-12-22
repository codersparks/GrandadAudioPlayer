﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace GrandadAudioPlayerClassLibrary.Configuration
{
    public class ConfigurationModel : ObservableObject
    {
        public string FolderPath { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
        public HashSet<string> AllowedExtensions { get; private set; }

        public ConfigurationModel()
        {
            this.AllowedExtensions = new HashSet<string>();
            this.AllowedExtensions.Add(".mp3");
            this.AllowedExtensions.Add(".m4a");
        }
    }
}

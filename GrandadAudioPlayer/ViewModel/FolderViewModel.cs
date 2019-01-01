﻿using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using GrandadAudioPlayer.Model.FolderView;
using GrandadAudioPlayer.Model.PlayList;
using GrandadAudioPlayer.Utils;
using GrandadAudioPlayer.Utils.Configuration;
// ReSharper disable RedundantArgumentDefaultValue
// ReSharper disable ExplicitCallerInfoArgument

namespace GrandadAudioPlayer.ViewModel
{
    public class FolderViewModel : ViewModelBase
    {

        private readonly ConfigurationManager _configurationManager;

        private FolderItemBase _selectedItem;
        public FolderItemBase SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (_selectedItem == value) return;

                _selectedItem = value;
                RaisePropertyChanged("SelectedItem");

                if (_selectedItem == null) return;

                var messageBody = new PlaylistMessage(_selectedItem.Path);
                Messenger.Default.Send(new NotificationMessage<PlaylistMessage>(messageBody, "Playlist Updated"));
            }
        } 

        public FolderViewModel(ConfigurationManager configurationManager)
        {
            _configurationManager = configurationManager;
            ReloadFolderStructure();
            Messenger.Default.Register < NotificationMessage<FolderMessage>>(this,  ReceiveFolderMessage);
        }

        public void ReloadFolderStructure()
        {
            RootFolder = new ObservableCollection<FolderItemBase>(FolderUtils.GetTreeStructure(_configurationManager.Configuration.FolderPath));
            if (RootFolder.Count > 0)
            {
                this.SelectedItem = RootFolder[0];
            }

            RaisePropertyChanged("RootFolder");
        }

        private void ReceiveFolderMessage(NotificationMessage<FolderMessage> message)
        {
            ReloadFolderStructure();
        }

        public ObservableCollection<FolderItemBase> RootFolder { get; private set; }

    }
}

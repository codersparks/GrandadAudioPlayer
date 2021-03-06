﻿using System;
using GrandadAudioPlayer.Views;
using log4net;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace GrandadAudioPlayer.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {

        private static readonly ILog _logger = LogManager.GetLogger(typeof(MainWindowViewModel));
        private readonly AdminView _adminView;
        private readonly PlaylistControlsViewModel _playlistControlsViewModel;

        public static string PlaylistContentRegion = "Player.Content";
        public static string TitleContentRegion = "Player.Title";
        public static string PlayerControlsContentRegion = "Player.Controls";

        public DelegateCommand OpenAdminDialogCommand { get; }
        public DelegateCommand MediaKeyNextCommand { get; }
        public DelegateCommand MediaKeyPreviousCommand { get; }
        public DelegateCommand MediaKeyPlayPauseCommand { get; }
        public DelegateCommand MediaKeyStopCommand { get; }

        public MainWindowViewModel(IRegionManager regionManager, AdminView adminView, PlaylistControlsViewModel playlistControlsViewModel)
        {
            _adminView = adminView;
            _playlistControlsViewModel = playlistControlsViewModel;
            regionManager.RegisterViewWithRegion(TitleContentRegion, typeof(TitleView));
            regionManager.RegisterViewWithRegion(PlaylistContentRegion, typeof(PlaylistView));
            regionManager.RegisterViewWithRegion(PlayerControlsContentRegion, typeof(PlaylistControlsView));

            OpenAdminDialogCommand = new DelegateCommand(OpenAdminDialogMethod);
            MediaKeyNextCommand = _playlistControlsViewModel.NextCommand;
            MediaKeyPreviousCommand = _playlistControlsViewModel.PreviousCommand;
            MediaKeyPlayPauseCommand = _playlistControlsViewModel.PlayPauseCommand;
            MediaKeyStopCommand = _playlistControlsViewModel.StopCommand;


        }

        public async void OpenAdminDialogMethod()
        {
            try
            {
                await DialogHost.Show(_adminView, "AdminDialog");
            }
            catch (InvalidOperationException e)
            {
                _logger.Error("Exception caught when opening dialog", e);
            }
        }

        

    }
}

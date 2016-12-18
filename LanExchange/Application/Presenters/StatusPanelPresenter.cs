using System;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Application.Presenters
{
    internal sealed class StatusPanelPresenter : PresenterBase<IStatusPanelView>, IStatusPanelPresenter
    {
        private readonly IShell32Service shellService;
        private readonly IScreenService screenService;
        private readonly ISystemInformationService systemInformation;
        private readonly IImageManager imageManager;

        public StatusPanelPresenter(
            IShell32Service shellService,
            IScreenService screenService,
            ISystemInformationService systemInformation,
            IImageManager imageManager)
        {
            if (shellService == null) throw new ArgumentNullException(nameof(shellService));
            if (screenService == null) throw new ArgumentNullException(nameof(screenService));
            if (systemInformation == null) throw new ArgumentNullException(nameof(systemInformation));
            if (imageManager == null) throw new ArgumentNullException(nameof(imageManager));

            this.shellService = shellService;
            this.screenService = screenService;
            this.systemInformation = systemInformation;
            this.imageManager = imageManager;
        }

        public void PerformComputerLeftClick()
        {
            shellService.OpenMyComputer();
        }

        public void PerformComputerRightClick(bool control, bool shift)
        {
            var position = screenService.CursorPosition;
            shellService.ShowMyComputerContextMenu(View.Handle, position, control, shift);
        }

        public void PerformUserRightClick()
        {
            // TODO implement context menu for user
        }

        protected override void InitializePresenter()
        {
            // show computer name
            View.ComputerName = systemInformation.ComputerName;
            View.ComputerImageIndex = imageManager.IndexOf(PanelImageNames.COMPUTER);

            // show current user
            View.UserName = systemInformation.UserName;
            View.UserImageIndex = imageManager.IndexOf(PanelImageNames.USER);
        }
    }
}
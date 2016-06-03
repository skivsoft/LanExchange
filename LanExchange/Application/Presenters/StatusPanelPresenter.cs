using System;
using System.Diagnostics.Contracts;
using LanExchange.Application.Interfaces;
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
            Contract.Requires<ArgumentNullException>(shellService != null);
            Contract.Requires<ArgumentNullException>(screenService != null);
            Contract.Requires<ArgumentNullException>(systemInformation != null);
            Contract.Requires<ArgumentNullException>(imageManager != null);

            this.shellService = shellService;
            this.screenService = screenService;
            this.systemInformation = systemInformation;
            this.imageManager = imageManager;
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

        public void PerformDoubleClick()
        {
            shellService.OpenMyComputer();
        }

        public void PerformComputerRightClick()
        {
            var position = screenService.CursorPosition;
            shellService.ShowMyComputerContextMenu(View.Handle, position);
        }

        public void PerformUserRightClick()
        {
            // TODO implement context menu for user
        }
    }
}
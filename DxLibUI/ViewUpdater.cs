using System;
using DxLibUI.Views;

namespace DxLibUI
{
    public class ViewUpdater
    {
        private ViewModel viewModel;

        internal event Action<ViewModel> Update;

        internal ViewModel CreateViewModel
        {
            get { return viewModel; }
            set { 
                viewModel = value;

                if (Update != null)
                {
                    Update(viewModel);
                }
            }
        }

    }
}

﻿using Ninject;
using Ninject.Parameters;
using Prism.Ioc;
using Prism.Mvvm;
using Xamarin.Forms;

namespace Prism.Ninject
{
    public abstract class PrismApplication : PrismApplicationBase
    {
        public PrismApplication(IPlatformInitializer initializer = null) 
            : base(initializer) { }

        protected override void ConfigureViewModelLocator()
        {
            ViewModelLocationProvider.SetDefaultViewModelFactory((view, type) =>
            {
                IParameter[] overrides = null;

                if (view is Page page)
                {
                    overrides = new IParameter[]
                    {
                        new ConstructorArgument("navigationService", CreateNavigationService(page))
                    };
                }

                return Container.GetContainer().Get(type, overrides);
            });
        }

        protected override IContainerExtension CreateContainerExtension()
        {
            return new NinjectContainerExtension(new StandardKernel());
        }
    }
}

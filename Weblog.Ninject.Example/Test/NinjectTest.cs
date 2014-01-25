using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using Ninject;
using NUnit.Framework;

namespace Weblog.Ninject.Example.Test
{   
    [TestFixture]
    public class NinjectTest
    {
        [Test]
        public void TestInjectSequenceDataProvider()
        {
            var kernel = new StandardKernel();
            kernel.Bind<IDataProvider>()
                .To<SequenceDataProvider>()
                .WithConstructorArgument("start", 1)
                .WithConstructorArgument("step", 1)
                .WithConstructorArgument("end", 5);

            var dataProvider = kernel.Get<IDataProvider>();

            dataProvider.Should().NotBeNull();
            dataProvider.Should().BeOfType<SequenceDataProvider>();
        }

        [Test]
        public void TestInjectSequenceDataProviderToDataProviderProxy()
        {
            var kernel = new StandardKernel();
            kernel.Bind<IDataProvider>()
                .To<SequenceDataProvider>()
                .WhenInjectedInto<IDataProvider>()
                .WithConstructorArgument("start", 1)
                .WithConstructorArgument("step", 1)
                .WithConstructorArgument("end", 5);

            kernel.Bind<IDataProvider>()
                .To<DataProviderProxy>();

            var dataProvider = kernel.Get<IDataProvider>();

            dataProvider.Should().NotBeNull();
            dataProvider.Should().BeOfType<DataProviderProxy>();
            dataProvider.As<DataProviderProxy>().DataProvider.Should().NotBeNull();
            dataProvider.As<DataProviderProxy>().DataProvider.Should().BeOfType<SequenceDataProvider>();
        }

        [Test]
        public void TestInjectDataProvidersToCombinedDataProvider()
        {
            var kernel = new StandardKernel();
            kernel.Bind<IDataProvider>()
                .To<CombinedDataProvider>()
                .When((request) =>
                {
                    if (request.Target != null)
                    {
                        if (typeof (IList<IDataProvider>).IsAssignableFrom(request.Target.Type))
                        {
                            return false;
                        }
                    }
                    return true;
                });

            kernel.Bind<IDataProvider>()
                .To<SequenceDataProvider>()
                .WhenInjectedInto<IDataProvider>()
                .WithConstructorArgument("start", 1)
                .WithConstructorArgument("step", 1)
                .WithConstructorArgument("end", 5);

            kernel.Bind<IDataProvider>()
                .To<RandomDataProvider>()
                .WhenInjectedInto<IDataProvider>();

            var dataProvider = kernel.Get<IDataProvider>();

            dataProvider.Should().NotBeNull();
            dataProvider.Should().BeOfType<CombinedDataProvider>();
            dataProvider.As<CombinedDataProvider>().DataProviders.Should().NotBeNull();
            dataProvider.As<CombinedDataProvider>().DataProviders.Count.ShouldBeEquivalentTo(2);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Autofac;
using Autofac.Extras.Moq;
using AutoFixture;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Business;
using DataAccess;
using Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    public class BaseUnitTest
    {
        protected AutoMock Mocker { get; set; }
        protected Fixture Fixture { get; set; }

        public BaseUnitTest()
        {
            Mocker = AutoMock.GetLoose();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Mocker.Dispose();
        }
    }
}

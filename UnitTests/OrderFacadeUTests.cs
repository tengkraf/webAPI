using Business;
using DataMgmtService.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class OrderFacadeUTests : BaseUnitTest
	{
        private IOrderFacade _orderFacade;

    //    [TestInitialize]
    //    public void TestInitialize()
    //    {
    //        _orderFacade = Mocker.Create<OrderFacade>();
    //    }

    //    [TestMethod]
    //    public async Task DoMetadataRecordsExist_WhenNotShowingOnlyUnapprovedRecords_ThenRecordsExist()
    //    {
    //        // Given

    //        // Mock the interface you want to change the functionality of
    //        // Call Setup and specify the method you want to mock the functionality of
    //        // Specify what parameters are allowed to trigger the mock result
    //        // Return the test data result
    //        Mocker.Mock<IExampleServiceClient>().Setup(x => x
    //            .ViewMetadataAsync(It.IsAny<bool>()))
    //            .Returns(10);

    //        bool showOnlyUnapprovedRecords = false;

    //        // When
    //        bool result = await _orderFacade.DoMetadataRecordsExist(showOnlyUnapprovedRecords);

    //        // Then
    //        Assert.IsTrue(result);
    //    }


    //    [TestMethod]
    //    public async Task DoMetadataRecordsExist_WhenShowingOnlyUnapprovedRecords_ThenRecordsDoNotExist()
    //    {
    //        // Given

    //        Mocker.Mock<IExampleServiceClient>().Setup(x => x
    //            .ViewMetadataAsync(It.IsAny<bool>()))
    //            .Returns(0);

    //        bool showOnlyUnapprovedRecords = true;

    //        // When
    //        bool result = await _orderFacade.DoMetadataRecordsExist(showOnlyUnapprovedRecords);

    //        // Then
    //        Assert.AreEqual(false, result);
    //    }
    }

}

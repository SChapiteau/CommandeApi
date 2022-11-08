using CommandApi.DAL;
using CommandApi.Entity;
using CommandApi.Entity.Interface;
using CommandApi.Process;
using CommandApi.Service;
using Moq;
using System.Net;

namespace CommandApi.test
{
    [TestClass]
    public class ValidateComemandProcessShould
    {
        

        

        [TestMethod]
        public void ReturnOk_When_StockIsAvailable_and_NewClient()
        {
            //Given
            var priceCalcilatorMock = new Mock<ICommandPriceCalculator>();
            var crmClientMock = new Mock<IClientCrm>();
            crmClientMock.Setup(c => c.GetClient(It.IsAny<int>())).Returns(new Client());
            var commandRepoMock = new Mock<ICommandRepository>();
            commandRepoMock.Setup(r => r.GetCommand(It.IsAny<int>())).Returns(new Command());
            var stockManagerMock = new Mock<ICommandStockManager>();
            stockManagerMock.Setup(s => s.IsStockAvailabelForCommand(It.IsAny<Command>())).Returns(true);
            

            ValidateComemandProcess _sut = new ValidateComemandProcess(priceCalcilatorMock.Object,
                crmClientMock.Object,
                commandRepoMock.Object,
                stockManagerMock.Object);

            //When
            var result = _sut.ValidateCommande(1);

            //Then
            Assert.IsTrue(result.IsSucces);
        }

        [TestMethod]
        public void Return_NotEnougthStock_When_StockIsNotAvailable()
        {
            //Given
            var priceCalcilatorMock = new Mock<ICommandPriceCalculator>();
            var crmClientMock = new Mock<IClientCrm>();
            crmClientMock.Setup(c => c.GetClient(It.IsAny<int>())).Returns(new Client());
            var commandRepoMock = new Mock<ICommandRepository>();
            commandRepoMock.Setup(r => r.GetCommand(It.IsAny<int>())).Returns(new Command());
            var stockManagerMock = new Mock<ICommandStockManager>();
            stockManagerMock.Setup(s => s.IsStockAvailabelForCommand(It.IsAny<Command>())).Returns(false);


            ValidateComemandProcess _sut = new ValidateComemandProcess(priceCalcilatorMock.Object,
                crmClientMock.Object,
                commandRepoMock.Object,
                stockManagerMock.Object);

            //When
            var result = _sut.ValidateCommande(1);

            //Then
            Assert.IsFalse(result.IsSucces);
        }
    }
}
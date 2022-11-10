using CommandApi.DAL;
using CommandApi.Entity;
using CommandApi.Entity.Interface;
using CommandApi.Process;
using CommandApi.Service;
using Moq;

namespace CommandApi.test
{
    [TestClass]
    public class ValidateCommandeProcessShould
    {
        

        

        [TestMethod]
        public void ReturnOk_When_StockIsAvailable_and_NewClient()
        {
            //Given
            var priceCalculatorMock = new Mock<ICommandePriceCalculator>();
            var crmClientMock = new Mock<IClientCrm>();
            crmClientMock.Setup(c => c.GetClient(It.IsAny<int>())).Returns(new Client());
            var commandRepoMock = new Mock<ICommandeRepository>();
            commandRepoMock.Setup(r => r.GetCommande(It.IsAny<int>())).Returns(new Commande() { Client = new Client() { Id = 1 } });
            var stockManagerMock = new Mock<ICommandStockManager>();
            stockManagerMock.Setup(s => s.IsStockAvailableForCommand(It.IsAny<Commande>())).Returns(true);
            

            ValidateCommandeProcess _sut = new ValidateCommandeProcess(priceCalculatorMock.Object,
                crmClientMock.Object,
                commandRepoMock.Object,
                stockManagerMock.Object);

            //When
            var result = _sut.ValidateCommande(1);

            //Then
            Assert.IsTrue(result.IsSucces);
        }

        [TestMethod]
        public void Return_NotEnoughStock_When_StockIsNotAvailable()
        {
            //Given
            var priceCalculatorMock = new Mock<ICommandePriceCalculator>();
            var crmClientMock = new Mock<IClientCrm>();
            crmClientMock.Setup(c => c.GetClient(It.IsAny<int>())).Returns(new Client());
            var commandRepoMock = new Mock<ICommandeRepository>();
            commandRepoMock.Setup(r => r.GetCommande(It.IsAny<int>())).Returns(new Commande());
            var stockManagerMock = new Mock<ICommandStockManager>();
            stockManagerMock.Setup(s => s.IsStockAvailableForCommand(It.IsAny<Commande>())).Returns(false);


            ValidateCommandeProcess _sut = new ValidateCommandeProcess(priceCalculatorMock.Object,
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
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;


namespace PaymentContext.Tests{
    [TestClass]
    public class CreateBoletoSubscriptionCommandTest{
        [TestMethod]
        public void ShouldReturnErrorWhenNameIsInvalid(){
           var command = new CreateBoletoSubscriptionCommand();
           command.FirstName = "";

           command.Validate();
           Assert.AreEqual(false, command.Valid); 
        }

    }
}
using System.ComponentModel.Design;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.ValueObjects;
using Flunt.Notifications;
using PaymentContext.Domain.Enums;


namespace PaymentContext.Tests{
    [TestClass]
    public class StudentTests{
        private readonly Name _name;
        private readonly Document _document;
        private readonly Email _email;
        private readonly Address _address;
        private readonly Student _student;
        private readonly Subscription _subscription;
        
        public StudentTests(){
            _name = new Name("Otavio", "Palhares");
            _document = new Document ("12345678901", EDocumentType.CPF);
            _email = new Email("otavio@gmail.com");
            _address = new Address("Rua 01", "33", "Advento", "Ueraba","MG","Brasil","2332343");
            _student = new Student(_name, _document, _email);
            _subscription = new Subscription(null);           

            
        }

        [TestMethod]
        public void ShouldReturnErrorWhenHadActiveSubscription(){
            var payment = new PayPalPayment("12244555",DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "Otavio", _document, _address, _email);
            _subscription.AddPayment(payment);
            _student.AddSubscription(_subscription);    
            _student.AddSubscription(_subscription);

            Assert.IsTrue(_student.Invalid);

        }

        [TestMethod]
        public void ShouldReturnErrorWhenHadNoActiveSubscriptionHasNoPayment(){
            
            _student.AddSubscription(_subscription);

            Assert.IsTrue(_student.Invalid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenAddSubscription(){
            var payment = new PayPalPayment("1224455535",DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "Otavio", _document, _address, _email);
            _subscription.AddPayment(payment);
            _student.AddSubscription(_subscription);            

            Assert.IsTrue(_student.Valid);

        }
    }
}
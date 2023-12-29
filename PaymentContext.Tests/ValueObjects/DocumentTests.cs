
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;


namespace PaymentContext.Tests{
    [TestClass]
    public class DocumentTests{
        [TestMethod]
        public void ShouldReturnErrorWhenCNPJIsInvalid(){
           var doc = new Document("123",EDocumentType.CNPJ);  
           Assert.IsTrue(doc.Invalid);
        }

         [TestMethod]
        public void ShouldReturnSuccessWhenCNPJIsValid(){
           var doc = new Document("12345678901234",EDocumentType.CNPJ);  
           Assert.IsTrue(doc.Valid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenCPFsInvalid(){
           var doc = new Document("123",EDocumentType.CPF);  
           Assert.IsTrue(doc.Invalid);
        }

         [TestMethod]
        public void ShouldReturnSuccessWhenCPFIsValid(){
            var doc = new Document("12345678901",EDocumentType.CPF);  
           Assert.IsTrue(doc.Valid);
        }

        
    }
}
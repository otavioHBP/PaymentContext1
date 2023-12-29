using PaymentContext.Domain.Enums;
using PaymentContext.Shared.ValueObejcts;
using Flunt.Validations;


namespace PaymentContext.Domain.ValueObjects{
    public class Document : ValueObejct{        
        public Document(string number, EDocumentType Type){
            
            Number = number;
            this.Type = Type;


            AddNotifications(new Contract()
                .Requires()
                .IsTrue(Validate(), "Document.Number", "Documento inválido.")
            );
        }

        public string Number { get; private set; }
        public EDocumentType Type { get; private set; }

        private bool Validate(){
            if (Type == EDocumentType.CNPJ && Number.Length == 14)
            return true;

            if (Type == EDocumentType.CPF && Number.Length == 11)
            return true;

            return false;
        }
    }
}
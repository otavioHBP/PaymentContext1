using PaymentContext.Shared.ValueObejcts;
using Flunt.Validations;

namespace PaymentContext.Domain.ValueObjects{
    public class Email : ValueObejct{        
        public Email(string address){
            
            Address = address;

            AddNotifications(new Contract()
                .Requires()
                .IsEmail(Address, "Email.Adress", "Email inválido")
            );    
            
        }

        public string Address { get; private set; }
        

    }
}
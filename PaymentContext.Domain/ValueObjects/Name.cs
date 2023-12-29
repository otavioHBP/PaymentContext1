using PaymentContext.Shared.ValueObejcts;
using Flunt.Notifications;
using Flunt.Validations;

namespace PaymentContext.Domain.ValueObjects{
    public class Name : ValueObejct{        
        public Name(string firstName, string lastName){
            
            FirstName = firstName;
            LastName = lastName;

             AddNotifications(new Contract()
                .Requires()
                .HasMinLen(FirstName,3,"Name.FisrtName","Nome deve conter pelo menos 3 caractéres")
                .HasMinLen(LastName,3,"Name.LastName","Sobrenome deve conter pelo menos 3 caractéres")
            );    
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public override string ToString()
        {
            return $"{FirstName}{LastName}";
        }

    }
}

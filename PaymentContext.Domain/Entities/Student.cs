using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using Microsoft.VisualBasic;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entity;
using Flunt.Validations;

namespace PaymentContext.Domain.Entities
{
    public class Student : Entity{

        private IList<Subscription>_subscriptions;
        public Student(Name name, Document document, Email email)
        {
            Name = name;
            Document = document;
            Email = email;
            _subscriptions = new List<Subscription>();
          
            AddNotifications(name, document, email);
        }

        public Name Name { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public Address Address { get; private set;  }
        public IReadOnlyCollection<Subscription> Subscriptions{ get {return _subscriptions.ToArray();} }

        public void AddSubscription(Subscription subscription){

            var hasSubscriptionActive = false;
            foreach (var sub in Subscriptions){
                if (sub.Active)
                    hasSubscriptionActive = true;             
            }

            AddNotifications(new Contract()
                .Requires()
                .IsFalse(hasSubscriptionActive, "Student.Subscriptions", "Você já possui uma assinatura ativa.")
                .AreEquals(0, subscription.Payments.Count, "Student.Subscriptions.Payments", "Esta assinatura não possui pagamento.")
            );
               
        }

    }
}
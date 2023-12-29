


using Flunt.Notifications;

namespace PaymentContext.Shared.Entity{

    public abstract class Entity : Notifiable
    {
        protected Entity()
        {   Id = Guid.NewGuid();
        }

        public Guid Id {get; set;}

        
    }

    
}
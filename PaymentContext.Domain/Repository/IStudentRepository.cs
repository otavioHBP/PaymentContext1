using PaymentContext.Domain.Entities;

namespace PaymentContext.Domain.Repository{
    public interface IStudentRepository{
        bool DocumentExists(string document);
        bool EmailExists( string email);
        void CreatSubscription(Student student);
    }
}
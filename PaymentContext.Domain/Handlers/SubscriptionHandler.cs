using System.Data;
using System.Diagnostics.Contracts;
using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repository;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers{
    public class SubscriptionHandler :
        Notifiable,
        IHandler<CreateBoletoSubscriptionCommand>,
        IHandler<CreatePayPalSubscriptionCommand>
    {
        private readonly IStudentRepository _repository;
        private readonly IEmailService _emailService;

        public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }


        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            //Fail Fast Validate
            command.Validate();
            if (command.Invalid){
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar seu cadastro");
            }


            //Verificar se o Documento já é cadastrado
            if (_repository.DocumentExists(command.Document)){
                AddNotification("Document","Este CPF já está em uso");
            }


            //Verificar se o Email já está cadastrado
            if (_repository.EmailExists(command.Email)){
                AddNotification("Email","Este E-mail já está em uso");
            }


            //Geras O VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document (command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City,command.State, command.Country, command.ZipCode);
         ;           

            //Gerar as entidades
            var student = new Student (name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1)); 
            var payment=  new BoletoPayment(command.BarCode, command.BoletoNumber, command.PaidDate, command.ExpireDate, command.Total, command.TotalPaid, command.Payer, new Document(command.PayerDocument, command.PayerDocumentType), address, email);


            //Relacionamento
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);


            //Agrupar notificações
            AddNotifications(name,document, email, address, student, subscription, payment);


            //Checar as notificações
            if(Invalid)
                return new CommandResult(false, "Não foi possivel realizar sua assinatura");


            //Salvar as Informações 
            _repository.CreatSubscription(student);


            //Enviar Email de boas vindas
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo", "Sua assinatura foi criada");

            //REtornar Informações
            return new CommandResult(true, "Assinatura realizada com sacesso!");
        }



        public ICommandResult Handle(CreatePayPalSubscriptionCommand command)
        {
            //Verificar se o Documento já é cadastrado
            if (_repository.DocumentExists(command.Document)){
                AddNotification("Document","Este CPF já está em uso");
            }


            //Verificar se o Email já está cadastrado
            if (_repository.EmailExists(command.Email)){
                AddNotification("Email","Este E-mail já está em uso");
            }


            //Geras O VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document (command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City,command.State, command.Country, command.ZipCode);
         ;           

            //Gerar as entidades
            var student = new Student (name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1)); 
            var payment=  new PayPalPayment(command.TransactionCode, command.PaidDate, command.ExpireDate, command.Total, command.TotalPaid, command.Payer, new Document(command.PayerDocument, command.PayerDocumentType), address, email);


            //Relacionamento
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);


            //Agrupar notificações
            AddNotifications(name,document, email, address, student, subscription, payment);


            //Salvar as Informações 
            _repository.CreatSubscription(student);


            //Enviar Email de boas vindas
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo", "Sua assinatura foi criada");

            //REtornar Informações
            return new CommandResult(true, "Assinatura realizada com sacesso!");
        }

    }
}

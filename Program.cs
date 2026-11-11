using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TravelAgencyCRM
{

    public interface IDocumentGenerator
    {
        byte[] Generate(Tour tour);
    }

    public interface IReminderRule
    {
        bool ShouldSend(Tour tour);
        string GetMessage(Tour tour);
        string GetRecipient(Tour tour);
        int Priority { get; }
    }

    public interface IEmailSender
    {
        void Send(string to, string subject, string message);
    }

    public abstract class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }

        public virtual string GetInfo() => $"{FullName} - {Position}";
    }

    public class Passport
    {
        public string Series { get; set; }
        public string Number { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string IssuedBy { get; set; }
    }

    public class Tourist
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Preferences { get; set; }

        public Passport Passport { get; set; }

        public List<Document> Documents { get; set; } = new List<Document>();

        public string GetPassportStatus()
        {
            if (Passport == null) return "Паспорт не указан";

            var daysUntilExpiry = (Passport.ExpiryDate - DateTime.Now).Days;

            if (daysUntilExpiry < 90) return "Требует срочной замены";
            if (daysUntilExpiry < 180) return "Требует замены";
            return "Действителен";
        }

        public List<Tour> ViewMyTrips()
        {
            return new List<Tour>();
        }

        public List<Document> ViewMyDocuments()
        {
            return Documents;
        }
    }

    public class TourService
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }

        public decimal GetPrice() => Price;
    }

    public class Payment
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public DateTime Date { get; set; }
        public string Method { get; set; }
        public string Direction { get; set; }

        public decimal GetAmount() => Amount;
    }

    public class Document
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Number { get; set; }
        public byte[] Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
        public int TouristId { get; set; }

        public string GetFileName() => $"{Type}_{Number}.pdf";
        public string GetMimeType() => "application/pdf";
    }

    public class Tour
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Destination { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }

        public Tourist Tourist { get; set; }

        public List<TourService> Services { get; set; } = new List<TourService>();
        public List<Payment> Payments { get; set; } = new List<Payment>();
        public List<Document> Documents { get; set; } = new List<Document>();

        public decimal CalculateTotal()
        {
            decimal total = 0;
            foreach (var service in Services)
            {
                total += service.Price;
            }
            return total;
        }

        public decimal CalculateProfit(decimal taxRate)
        {
            return 0;
        }

        public void RegisterPayment(Payment payment)
        {
            Payments.Add(payment);
            UpdateStatus();
        }

        public decimal GetRemainingDebt()
        {
            decimal total = CalculateTotal();
            decimal paid = 0;

            foreach (var payment in Payments)
            {
                paid += payment.Amount;
            }

            decimal debt = total - paid;
            return debt;
        }

        private void UpdateStatus()
        {
            decimal debt = GetRemainingDebt();
            if (debt <= 0)
                Status = "Оплачен полностью";
            else
                Status = "Частично оплачен";
        }
    }

    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Contacts { get; set; }
        public string BankDetails { get; set; }

        public List<Tour> GetOrderHistory()
        {
            return new List<Tour>();
        }
    }

    public class Manager : Employee
    {
        public void CreateTour(Tour tour)
        {

        }

        public void EditTour(Tour tour)
        {
           
        }

        public List<Tourist> FindTourist(string query)
        {

            return new List<Tourist>();
        }

        public List<Document> GenerateDocuments(Tour tour, string[] types, DocumentService docService)
        {
            return docService.GenerateDocuments(tour, types);
        }
    }

    public class Accountant : Employee
    {
        public void RegisterPayment(Tour tour, Payment payment)
        {
            tour.RegisterPayment(payment);
        }

        public decimal ViewDebts(List<Tour> tours)
        {
            decimal totalDebt = 0;

            foreach (var tour in tours)
            {
                decimal debt = tour.GetRemainingDebt();

                if (debt > 0)
                {
                    totalDebt += debt;
                }
            }
            return totalDebt;
        }

        public string GenerateReport()
        {
            return "";
        }
    }

    public class Administrator : Employee
    {
        public void ManageSuppliers(Supplier supplier)
        {

        }

        public void ConfigureReminderRules(NotificationService service, List<IReminderRule> rules)
        {

        }
    }

    public class ContractGenerator : IDocumentGenerator
    {
        public byte[] Generate(Tour tour)
        {
            byte[] bits = new byte[10];
            return bits;
        }
    }

    public class BillGenerator : IDocumentGenerator
    {
        public byte[] Generate(Tour tour)
        {
            byte[] bits = new byte[10];
            return bits;
        }
    }

    public class InvoiceGenerator : IDocumentGenerator
    {
        public byte[] Generate(Tour tour)
        {
            byte[] bits = new byte[10];
            return bits;
        }
    }

    public class VoucherGenerator : IDocumentGenerator
    {
        public byte[] Generate(Tour tour)
        {
            byte[] bits = new byte[10];
            return bits;
        }
    }

    public class MemoGenerator : IDocumentGenerator
    {
        public byte[] Generate(Tour tour)
        {
            byte[] bits = new byte[10];
            return bits;
        }
    }

    public class ReceiptGenerator : IDocumentGenerator
    {
        public byte[] Generate(Tour tour)
        {
            byte[] bits = new byte[10];
            return bits;
        }
    }

    public class FlightReminderRule : IReminderRule
    {
        private int daysBefore { get; }

        public int Priority => 1;

        public bool ShouldSend(Tour tour)
        {
            return true;
        }

        public string GetMessage(Tour tour)
        {
            return "";
        }

        public string GetRecipient(Tour tour)
        {
            return "";
        }
    }

    public class PassportExpiryRule : IReminderRule
    {
        public int Priority => 2;

        public bool ShouldSend(Tour tour)
        {
            return true;
        }

        public string GetMessage(Tour tour)
        {
            return "";
        }

        public string GetRecipient(Tour tour)
        {
            return "";
        }
    }

    public class DebtReminderRule : IReminderRule
    {
        private decimal minDebt { get; }

        public int Priority => 3;

        public bool ShouldSend(Tour tour)
        {
            return true;
        }

        public string GetMessage(Tour tour)
        {
            return "";
        }

        public string GetRecipient(Tour tour)
        {
            return "";
        }
    }

    public class EmailSender : IEmailSender
    {
        private string smtpHost { get; }
        private int smtpPort { get; }

        public void Send(string to, string subject, string message)
        {

        }
    }

    public class DocumentService
    {
        private readonly Dictionary<string, IDocumentGenerator> generators;
        private readonly List<Document> documents = new List<Document>();
        private static int nextId = 1;

        public DocumentService()
        {
            generators = new Dictionary<string, IDocumentGenerator>
            {
                { "Contract", new ContractGenerator() },
                { "Bill", new BillGenerator() },
                { "Invoice", new InvoiceGenerator() },
                { "Voucher", new VoucherGenerator() },
                { "Memo", new MemoGenerator() },
                { "Receipt", new ReceiptGenerator() }
            };
        }
        public List<Document> GenerateDocuments(Tour tour, string[] types)
        {
            var generatedDocs = new List<Document>();

            foreach (var type in types)
            {
                if (!generators.ContainsKey(type))
                    throw new ArgumentException($"Неизвестный тип: {type}");

                var generator = generators[type];

                byte[] content = generator.Generate(tour);

                Document doc = new()
                {
                    Id = nextId++,
                    Type = type,
                    Number = $"{type}-{tour.Number}",
                    Content = content,
                    CreatedAt = DateTime.Now,
                    Status = "Сформирован",
                    TouristId = tour.Tourist?.Id ?? 0   
                };
    
                documents.Add(doc);
                tour.Documents.Add(doc);

                if (tour.Tourist != null)
                {
                    tour.Tourist.Documents.Add(doc);
                }

                generatedDocs.Add(doc);
            }

            return generatedDocs;
        }

        public Document GetDocument(int id)
        {
            return documents.FirstOrDefault(d => d.Id == id);
        }

        public List<Document> GetTouristDocuments(int touristId)
        {
            return documents.Where(d => d.TouristId == touristId).ToList();
        }

        public byte[] DownloadDocument(int id)
        {
            var doc = GetDocument(id);
            if (doc == null) throw new ArgumentException("Документ не найден");
            return doc.Content;
        }
    }

    public class NotificationService
    {
        private readonly List<IReminderRule> rules = new List<IReminderRule>();
        private IEmailSender emailSender { get; }

        public void SendReminders(List<Tour> tours)
        {

            var sortedRules = rules.OrderBy(r => r.Priority).ToList();

            foreach (var tour in tours)
            {

                foreach (var rule in sortedRules)
                {
                    bool shouldSend = rule.ShouldSend(tour);

                    if (shouldSend)
                    {
                        string recipient = rule.GetRecipient(tour);
                        string message = rule.GetMessage(tour);
                        string subject = $"Напоминание по туру {tour.Number}";
                        emailSender.Send(recipient, subject, message);
                    }
                }
            }
        }

        public void AddRule(IReminderRule rule)
        {
            rules.Add(rule);
        }

        public void RemoveRule(IReminderRule rule)
        {
            rules.Remove(rule);
        }
    }
}
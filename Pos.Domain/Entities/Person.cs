using Pos.Domain.Entities.Common;
using Pos.Domain.ValueObjects.Person;

namespace Pos.Domain.Entities
{
    public class Person : BaseEntity
    {
        public Guid DocumentTypeId { get; private set; }
        public DocumentNumber DocumentNumber { get; private set; } = default!;
        public Name Name { get; private set; } = default!;
        public Address? Address { get; private set; }
        public Email? Email { get; private set; }
        public Phone? Phone { get; private set; }

        public DocumentType DocumentType { get; private set; } = default!;

        protected Person() { }

        private Person(DocumentType documentType, DocumentNumber documentNumber, Name name, Address? address, Email? email, Phone? phone)
        {
            DocumentType = documentType;
            DocumentTypeId = documentType.Id;
            DocumentNumber = documentNumber;
            Name = name;
            Address = address;
            Email = email;
            Phone = phone;
        }

        public static Person Create(DocumentType documentType, string documentNumber, string name, string? address, string? email, string? phone)
        {
            var documentNumberVO = DocumentNumber.Create(documentNumber, documentType);
            var nameVO = Name.Create(name);
            var addressVO = Address.Create(address);
            var emailVO = Email.Create(email);
            var phoneVO = Phone.Create(phone);

            return new(documentType, documentNumberVO, nameVO, addressVO, emailVO, phoneVO);
        }

        public void UpdateDocumentType(DocumentType documentType)
        {
            DocumentType = documentType;
            DocumentTypeId = documentType.Id;
        }

        public void UpdateDocumentNumber(string documentNumber, DocumentType documentType)
        {
            DocumentNumber = DocumentNumber.Create(documentNumber, documentType);
        }

        public void UpdateName(string name)
        {
            Name = Name.Create(name);
        }

        public void UpdateAddress(string? address)
        {
            Address = Address.Create(address);
        }

        public void UpdateEmail(string? email)
        {
            Email = Email.Create(email);
        }

        public void UpdatePhone(string? phone)
        {
            Phone = Phone.Create(phone);
        }
    }
}

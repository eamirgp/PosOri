namespace Pos.Domain.ValueObjects.Purchase
{
    public class IssueDate
    {
        public DateTime Value { get; private set; }

        private IssueDate(DateTime value)
        {
            Value = value;
        }

        public static IssueDate Create(DateTime value)
        {
            var today = DateTime.Now.Date;

            if (value.Date > today)
                throw new ArgumentException("La fecha de emisión no puede ser futura.");

            return new(value);
        }
    }
}

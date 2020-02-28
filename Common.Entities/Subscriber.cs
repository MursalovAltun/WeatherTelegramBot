namespace Common.Entities
{
    public class Subscriber : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string WaitingFor { get; set; }

        public long ChatId { get; set; }

        public string City { get; set; }

        public string Language { get; set; }

        public int UtcOffset { get; set; }

        public virtual SubscriberSettings Settings { get; set; }
    }
}
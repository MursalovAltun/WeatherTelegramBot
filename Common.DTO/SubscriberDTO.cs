using System;

namespace Common.DTO
{
    public class SubscriberDTO
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string WaitingFor { get; set; }

        public bool IsDelete { get; set; }
    }
}
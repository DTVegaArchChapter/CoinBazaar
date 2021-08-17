namespace CoinBazaar.Infrastructure.EventBus
{
    using System;

    public sealed class EventSourceMetadata
    {
        public Guid StreamId { get; set; }

        public bool ProcessStarter { get; set; }

        public DateTime CreationDate { get; set; }

        public string CreatedBy { get; set; }

        public EventSourceMetadata(Guid streamId, bool processStarter, DateTime creationDate, string createdBy)
        {
            StreamId = streamId;
            ProcessStarter = processStarter;
            CreationDate = creationDate;
            CreatedBy = createdBy;
        }
    }
}

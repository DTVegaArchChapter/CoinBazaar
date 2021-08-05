using System;

namespace CoinBazaar.Infrastructure.EventBus
{
    public class ESMetadata
    {
        public Guid StreamId { get; set; }
        public bool ProcessStarter { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreatedBy { get; set; }

        public ESMetadata(Guid streamId, bool processStarter, DateTime creationDate, string createdBy)
        {
            StreamId = streamId;
            ProcessStarter = processStarter;
            CreationDate = creationDate;
            CreatedBy = createdBy;
        }
    }
}

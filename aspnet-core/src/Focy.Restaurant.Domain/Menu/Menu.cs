using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Focy.Restaurant.Menu
{
    public class Menu : FullAuditedEntity<Guid>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImgUri { get; set; }
        public string? Uri { get; set; }
        public bool IsAvailable { get; set; }

        protected Menu() { }

        public Menu(Guid id, string name, string? description, string? imgUri, string? uri, bool isAvailable)
            : base(id)
        {
            Id = id;
            Name = name;
            Description = description;
            ImgUri = imgUri;
            Uri = uri;
            IsAvailable = isAvailable;
        }
    }
}

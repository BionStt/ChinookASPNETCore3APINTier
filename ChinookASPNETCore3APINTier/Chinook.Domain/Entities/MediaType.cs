﻿using Chinook.Domain.Converters;
using Chinook.Domain.ApiModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chinook.Domain.Entities
{
    public class MediaType : IConvertModel<MediaType, MediaTypeApiModel>
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int MediaTypeId { get; set; }

        public string Name { get; set; }

        [NotMapped]
        [Newtonsoft.Json.JsonIgnore]
        public ICollection<Track> Tracks { get; set; } = new HashSet<Track>();

        [NotMapped]
        [Newtonsoft.Json.JsonIgnore]
        public MediaTypeApiModel Convert => new MediaTypeApiModel
        {
            MediaTypeId = MediaTypeId,
            Name = Name
        };
    }
}
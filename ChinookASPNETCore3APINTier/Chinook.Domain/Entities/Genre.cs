﻿using Chinook.Domain.Converters;
using Chinook.Domain.ApiModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chinook.Domain.Entities
{
    public class Genre : IConvertModel<Genre, GenreApiModel>
    {

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int GenreId { get; set; }

        public string Name { get; set; }

        [NotMapped]
        [Newtonsoft.Json.JsonIgnore]
        public ICollection<Track> Tracks { get; set; } = new HashSet<Track>();

        [NotMapped]
        [Newtonsoft.Json.JsonIgnore]
        public GenreApiModel Convert => new GenreApiModel
        {
            GenreId = GenreId,
            Name = Name
        };
    }
}
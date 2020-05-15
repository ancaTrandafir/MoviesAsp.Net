using Movies.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Movies.Models
{

    public enum Genre
    {
        Adventure,
        Comedy,
        Horror,
        SciFi
    }


    public class Movie
    {

        public long ID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

        [StringLength(150, MinimumLength = 3)]
        public string Description { get; set; }

        public Genre Genre { get; set; }

        public int Duration { get; set; }

        [Range(1900, 2020)]
        public int YearOfRelease { get; set; }

        [StringLength(30, MinimumLength = 3)]
        public string Director { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateAdded { get; set; }

        [Range(1, 10)]
        public int Rating { get; set; }

        public bool Watched { get; set; }

      
    }

}

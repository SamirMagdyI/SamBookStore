﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SamBookStore.Models.Domain
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        [Required]
        public string Isbn { get; set; }
     
        [Required]
        public int TotalPages { get; set; }

        [Required]
        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        [Required]
        [ForeignKey("Publisher")]
        public int PubhlisherId { get; set; }
        [Required]
        [ForeignKey("Genre")]
        public int GenreId { get; set; }

        public Author Author { get; set; }
        public Publisher Publisher { get; set; }
        public Genre Genre { get; set; }

        [NotMapped]
        public string ? AuthorName { get; set; }
        [NotMapped]
        public string ? PublisherName { get; set; }
        [NotMapped]
        public string ? GenreName { get; set; }

        [NotMapped]
        public List<SelectListItem> ? AuthorList { get; set; }
        [NotMapped]
        public List<SelectListItem>? PublisherList { get; set; }
        [NotMapped]
        public List<SelectListItem> ? GenreList { get; set; }

    }
}

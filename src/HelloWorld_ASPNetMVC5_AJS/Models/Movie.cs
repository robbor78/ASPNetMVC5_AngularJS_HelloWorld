﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorld_ASPNetMVC5_AJS.Models
{
  public class Movie
  {
    public int Id { get; set; }

    [Required(ErrorMessage = "Movie Title is Required")]
    [MinLength(3, ErrorMessage = "Movie Title must be at least 3 characters")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Movie Director is Required.")]
    public string Director { get; set; }

    [Range(0, 100, ErrorMessage = "Ticket price must be between 0 and 100 dollars.")]
    public decimal TicketPrice { get; set; }

    [Required(ErrorMessage = "Movie Release Date is required")]
    public DateTime ReleaseDate { get; set; }
  }
}

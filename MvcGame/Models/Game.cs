using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcGame.Models;

public class Game
{
    public int Id { get; set; }
    [StringLength(60, MinimumLength = 3)]
    public string Tytul { get; set; }
    [Display(Name = "Data wydania"), DataType(DataType.Date)]
    public DateTime Data_wydania { get; set; }
    [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$"), Required, StringLength(30)]
    public string Gatunek { get; set; }
    [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$"), StringLength(60)]
    public string Wydawca { get; set; }
    [Range(1, 100), DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Cena { get; set; }

}
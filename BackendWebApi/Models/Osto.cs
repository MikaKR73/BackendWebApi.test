using System;
using System.Collections.Generic;

namespace BackendWebApi.Models;

public partial class Osto
{
    public int Id { get; set; }

    public string Tuote { get; set; } = null!;

    public decimal Hinta { get; set; }

    public DateOnly Päivä { get; set; }

    public string Kauppa { get; set; } = null!;

    public string? Osoite { get; set; }

    public string? Tietoja { get; set; }
}

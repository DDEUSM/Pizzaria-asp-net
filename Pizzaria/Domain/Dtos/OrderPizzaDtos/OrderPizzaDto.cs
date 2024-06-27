using System.ComponentModel.DataAnnotations;

public class OrderPizzaDto
{
    public Guid? Id { get; set;}

    public Guid? OrderId { get; set;}

    [Required, MinLength(9), MaxLength(75)]
    public string FirstPizzaId { get; set;}

    [Required, MinLength(9), MaxLength(75)]
    public string SecondPizzaId { get; set;}
}
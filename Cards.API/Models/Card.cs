using System.ComponentModel.DataAnnotations;

namespace Cards.API.Models
{
    public class Card
    {
        [Key]
        public Guid Id { get; set; }

        public string CardHolderNumber { get; set; }

        public string CardNumber { get; set; }

        public int Expirymonth { get; set; }

        public int Expiryyear { get; set;}

        public int CVC { get; set; }
    }
}

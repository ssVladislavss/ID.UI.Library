using System.ComponentModel.DataAnnotations;

namespace ID.UI.Core.Clients.Models
{
    public class ClientClaimModel
    {
        [Required(ErrorMessage = "Поле - Type - обязательно к заполнению")]
        public string Type { get; set; } = string.Empty;
        [Required(ErrorMessage = "Поле - Value - обязательно к заполнению")]
        public string Value { get; set; } = string.Empty;
    }
}

using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Commerce
{
    public class UpdateCartItemRequest
    {
        public Guid CartItemId { get; set; }
        public int Quantity { get; set; }
    }
}
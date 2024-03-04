using CoreMVCCodeFirst_1.Models.Entities;

namespace CoreMVCCodeFirst_1.Models.ViewModels.ProductPageVms
{
    public class CreateUpdateProductPageVM
    {
        public List<Category> Categories { get; set; }
        public Product Product { get; set; }

    }
}

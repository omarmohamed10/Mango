namespace Mango.Web.Models
{
    public class CouponDTO
    {
        public int CouponId { get; set; }
        public string CouponCode { get; set; }
        public double DicountAmount { get; set; }
        public int MinAmount { get; set; }
    }
}


using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EPRPlatform.API.Dto.BaseModels
{
    public class BSInvenAdd
    {
        /// <summary>
        /// 存货编码
        /// </summary>
        [DisplayName("数据id")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public string InvenCode { get; set; }
        /// <summary>
        /// 存货名称
        /// </summary>
        [DisplayName("存货名称")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public string InvenName { get; set; }
        /// <summary>
        /// 存货类别
        /// </summary>
        [DisplayName("存货类别")]
        [Required(ErrorMessage = "请选择{0}")]
        public string InvenTypeCode { get; set; }
        /// <summary>
        /// 规格型号
        /// </summary>
        [DisplayName("规格型号")]
        public string SpecsModel { get; set; }
        /// <summary>
        /// 计量单位
        /// </summary>
        [DisplayName("计量单位")]
        public string MeaUnit { get; set; }
        /// <summary>
        /// 参考售价
        /// </summary>
        [DisplayName("参考售价")]
        public decimal? SelPrice { get; set; }
        /// <summary>
        /// 参考进价
        /// </summary>
        [DisplayName("参考进价")]
        public decimal? PurPrice { get; set; }
        /// <summary>
        /// 最低库存
        /// </summary>
        [DisplayName("最低库存")]
        public int? SmallStockNum { get; set; }
        /// <summary>
        /// 最高库存
        /// </summary>
        [DisplayName("最高库存")]
        public int? BigStockNum { get; set; }
    }
    public class BSInvenUpdate : BSInvenAdd
    {
        /// <summary>
        /// 数据id
        /// </summary>
        [DisplayName("数据id")]
        [Required(ErrorMessage = "请输入{0}")]
        public long Id { get; set; }
    }
}

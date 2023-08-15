using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MicroService.Dto.OAModels.CustomModule
{
    public class PropertyValueAdd
    {
        public List<PropertyValueAddData> Datas { get; set; }
    }
    public class PropertyValueAddData
    {
        /// <summary>
        /// 属性id
        /// </summary>
        [DisplayName("属性id")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public int PropertyId { get; set; }
        /// <summary>
        /// 属性值
        /// </summary>
        [DisplayName("属性值")]
        public string Value { get; set; }
    }
    public class PropertyValueUpdate
    {
        public List<PropertyValueUpdateData> Datas { get; set; }
    }
    public class PropertyValueUpdateData
    {
        /// <summary>
        /// 数据id
        /// </summary>
        [DisplayName("数据id")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public int Id { get; set; }
        /// <summary>
        /// 属性id
        /// </summary>
        [DisplayName("属性id")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public int PropertyId { get; set; }
        /// <summary>
        /// 属性值
        /// </summary>
        [DisplayName("属性值")]
        public string Value { get; set; }
    }
}

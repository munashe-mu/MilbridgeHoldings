namespace MilbridgeHoldings.Models.Data.Local
{
    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field)]
    public class EnumDescriptionAttribute : Attribute
    {
        private readonly string description;

        public EnumDescriptionAttribute(string description) : base() => this.description = description;

        public string Description
        {
            get
            {
                return description;
            }
        }
    }
}

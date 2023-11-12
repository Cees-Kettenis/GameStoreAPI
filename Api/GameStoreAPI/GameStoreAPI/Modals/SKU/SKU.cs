namespace GameStoreAPi.Modals.SKU
{
    public class SKU : DomainObject
    {
        public string number { get; set; } = string.Empty;
        public string name { get;set; } = string.Empty;
        public string barcode { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public Decimal rating { get; set; } = 0;
        public Decimal stock { get; set; } = 0;
    }
}

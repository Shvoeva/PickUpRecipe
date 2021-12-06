namespace PickUpRecipe.Core.SiteWithDish
{
    class SiteWithDishSetting : IParserSettings
    {
        public SiteWithDishSetting(string address)
        {
            BaseUrl = address;
        }

        public string BaseUrl { get; set; }
    }
}

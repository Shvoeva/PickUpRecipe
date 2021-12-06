namespace PickUpRecipe.Core.RecipeSite
{
    class RecipeSiteSettings : IParserSettings
    {
        public RecipeSiteSettings(string address)
        {
            BaseUrl = address;
        }

        public string BaseUrl { get; set; }
    }
}

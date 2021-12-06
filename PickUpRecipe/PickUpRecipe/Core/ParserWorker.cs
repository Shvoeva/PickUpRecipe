using System;
using System.Threading.Tasks;
using AngleSharp.Html.Parser;
//value

namespace PickUpRecipe.Core
{
    class ParserWorker<T> where T : class
    {
        IParser<T> parser;
        IParserSettings parserSettings;

        HtmlLoader loader;

        bool isActive;

        public event Action<object, T> OnNewData;
        public event Action<object> OnCompleted;

        public IParser<T> Parser
        {
            get
            {
                return parser;
            }
            set
            {
                parser = value;
            }
        }

        public IParserSettings Settings
        {
            get
            {
                return parserSettings;
            }
            set
            {
                parserSettings = value;
                loader = new HtmlLoader(value);
            }
        }

        public bool IsActive
        {
            get
            {
                return isActive;
            }
        }

        public ParserWorker(IParser<T> parser)
        {
            this.parser = parser;
        }

        public ParserWorker(IParser<T> parser, IParserSettings parserSettings) : this(parser)
        {
            this.parserSettings = parserSettings;
        }

        public async Task Start()
        {
            isActive = true;
            await Worker();
        }

        public void Abort()
        {
            isActive = false;
        }

        private async Task Worker()
        {
            if (!isActive)
            {
                OnCompleted?.Invoke(this);
                return;
            }

            var source = await loader.GetSourceByPageId();
            var domParser = new HtmlParser();
            var document = domParser.ParseDocument(source);
            var result = parser.Parse(document);

            OnNewData?.Invoke(this, result);

            OnCompleted?.Invoke(this);
            Abort();
        }
    }
}

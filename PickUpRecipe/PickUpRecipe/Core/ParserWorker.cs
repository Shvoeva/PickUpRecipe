using System;
using System.Threading.Tasks;
using AngleSharp.Html.Parser;
//value

namespace PickUpRecipe.Core
{
	/// <summary>
	/// Класс для работы с парсом.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class ParserWorker<T> where T : class
	{
		/// <summary>
		/// Парсер.
		/// </summary>
		IParser<T> _parser;

		/// <summary>
		/// Настройки парсера.
		/// </summary>
		IParserSettings _parserSettings;

		/// <summary>
		/// HTML loader.
		/// </summary>
		HtmlLoader _loader;

		/// <summary>
		/// Флаг, отвечающий за работу парсера.
		/// </summary>
		bool _isActive;

		/// <summary>
		/// Событие появление новых данных.
		/// </summary>
		public event Action<object, T> OnNewData;

		/// <summary>
		/// Событие окончания парса.
		/// </summary>
		public event Action<object> OnCompleted;

		/// <summary>
		/// Возвращает и задает парсер.
		/// </summary>
		public IParser<T> Parser
		{
			get => _parser;
			set => _parser = value;
		}

		/// <summary>
		/// Возвращает и задает настройки парсера.
		/// </summary>
		public IParserSettings ParserSettings
		{
			get => _parserSettings;
			set
			{
				_parserSettings = value;
				_loader = new HtmlLoader(value);
			}
		}

		/// <summary>
		/// Возвращает значение флага.
		/// </summary>
		public bool IsActive => _isActive;

		/// <summary>
		/// Конструктор.
		/// </summary>
		/// <param name="parser">Парсер.</param>
		public ParserWorker(IParser<T> parser)
		{
			Parser = parser;
		}

		/// <summary>
		/// Конструктор.
		/// </summary>
		/// <param name="parser">Парсер.</param>
		/// <param name="parserSettings">Настройки парсера.</param>
		public ParserWorker(IParser<T> parser, IParserSettings parserSettings) : this(parser)
		{
			ParserSettings = parserSettings;
		}

		/// <summary>
		/// Начало парса.
		/// </summary>
		/// <returns></returns>
		public async Task Start()
		{
			_isActive = true;
			await Parse();
		}

		/// <summary>
		/// Остановка парса.
		/// </summary>
		public void Abort()
		{
			_isActive = false;
		}

		/// <summary>
		/// Парс.
		/// </summary>
		/// <returns></returns>
		private async Task Parse()
		{
			if (!_isActive)
			{
				OnCompleted?.Invoke(this);
				return;
			}

			var source = await _loader.GetSourceByPageId();
			var domParser = new HtmlParser();
			var document = domParser.ParseDocument(source);
			var result = _parser.Parse(document);
			OnNewData?.Invoke(this, result);
			OnCompleted?.Invoke(this);
			Abort();
		}
	}
}

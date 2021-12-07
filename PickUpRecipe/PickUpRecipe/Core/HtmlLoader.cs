using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PickUpRecipe.Core.ParserSettings;

namespace PickUpRecipe.Core
{
	/// <summary>
	/// HTML загрузчик.
	/// </summary>
	public class HtmlLoader
	{
		/// <summary>
		/// Длина строки.
		/// </summary>
		private const int StringLength = 4096;

		/// <summary>
		/// HTTP клиент.
		/// </summary>
		private readonly HttpClient _client;

		/// <summary>
		/// Ссылка на сайт.
		/// </summary>
		private readonly string _url;

		/// <summary>
		/// Конструктор.
		/// </summary>
		/// <param name="settings">Настройки парсера.</param>
		public HtmlLoader(IParserSettings settings)
		{
			_client = new HttpClient();
			_url = settings.BaseUrl;
		}

		/// <summary>
		/// Получить код страницы.
		/// </summary>
		/// <returns>Код страницы типа <see cref="string"/>.</returns>
		public async Task<string> GetSourceByPageId()
		{
			var response = await _client.GetAsync(_url);

			if (response == null || response.StatusCode != HttpStatusCode.OK) return null;
			var responseLength = (int)response.Content.Headers.ContentLength;
			var sb = new StringBuilder(capacity: responseLength);
			using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
			{
				using (var rdr = new StreamReader(responseStream))
				{
					var charBuffer = new char[StringLength];

					while (true)
					{
						var read = await rdr.ReadAsync(charBuffer, 0, charBuffer.Length).ConfigureAwait(false);
						sb.Append(charBuffer, 0, read);

						if (read == 0)
						{
							break;
						}
					}
				}

			}

			return sb.ToString();

		}
	}
}

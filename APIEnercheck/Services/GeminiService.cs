using System.Text.Json;
using System.Text;

namespace APIEnercheck.Services
{
    public class GeminiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _baseUrl = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-pro:generateContent";

        public GeminiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = Environment.GetEnvironmentVariable("GEMINI_API_KEY");
        }

        public async Task<string> AnalisarImagemAsync(byte[] imageBytes, string mimeType, string descricaoProjeto)
        {
            var base64Image = Convert.ToBase64String(imageBytes);

            // passando o schema em formato que o google genAI aceite (loucura insana rapaz) - é um json feito em c#
            var schema = new
            {
                type = "OBJECT",
                properties = new
                {
                    analiseCategorizada = new
                    {
                        type = "ARRAY",
                        items = new
                        {
                            type = "OBJECT",
                            properties = new
                            {
                                categoria = new { type = "STRING" },
                                percentualConformidade = new { type = "INTEGER" },
                                conformidades = new
                                {
                                    type = "ARRAY",
                                    items = new
                                    {
                                        type = "OBJECT",
                                        properties = new
                                        {
                                            item = new { type = "STRING" },
                                            observacao = new { type = "STRING" }
                                        },
                                        required = new[] { "item", "observacao" }
                                    }
                                },
                                naoConformidadesOuVerificar = new
                                {
                                    type = "ARRAY",
                                    items = new
                                    {
                                        type = "OBJECT",
                                        properties = new
                                        {
                                            item = new { type = "STRING" },
                                            observacao = new { type = "STRING" }
                                        },
                                        required = new[] { "item", "observacao" }
                                    }
                                }
                            },
                            required = new[] { "categoria", "percentualConformidade", "conformidades", "naoConformidadesOuVerificar" }
                        }
                    }
                },
                required = new[] { "analiseCategorizada" }
            };

            // montando a resposta
            var requestBody = new
            {
                // Instrução de Sistema
                system_instruction = new
                {
                    parts = new[] { new { text = "Você é um engenheiro eletricista especialista em normas técnicas brasileiras, focado na ABNT NBR 5410. Sua tarefa é analisar a planta baixa elétrica fornecida e avaliar sua conformidade, estruturando a resposta em categorias com percentuais de conformidade.\r\n\r\nAnalise a planta e divida sua avaliação nas seguintes categorias:\r\n1.  **Condutores e Circuitos:**\r\n    *   Verifique a bitola dos fios para iluminação (mín. 1,5 mm²), TUGs (mín. 2,5 mm²) e TUEs.\r\n    *   Verifique a separação de circuitos de iluminação e tomadas.\r\n    *   Verifique se equipamentos de alta potência (>10A) têm circuitos dedicados (TUEs).\r\n2.  **Pontos de Utilização:**\r\n    *   Verifique a quantidade e o posicionamento de TUGs em salas/quartos (1 a cada 5m de perímetro) e cozinhas/áreas de serviço (1 a cada 3,5m).\r\n    *   Verifique a altura dos pontos de tomada (baixas, médias, altas), se indicado.\r\n3.  **Proteção e Segurança:**\r\n    *   Identifique a presença de disjuntores para cada circuito.\r\n    *   Verifique a indicação de Dispositivos DR para áreas molhadas.\r\n    *   Verifique a presença do condutor de proteção (terra) em todos os pontos.\r\n4.  **Simbologia e Documentação:**\r\n    *   Avalie se a simbologia está consistente com os padrões da ABNT.\r\n    *   Verifique se há um quadro de cargas ou legendas claras.\r\n\r\nPara CADA categoria, você deve:\r\na) Calcular um 'percentualConformidade' (0 a 100) baseado em quantos sub-itens daquela categoria foram atendidos.\r\nb) Criar uma lista de 'conformidades' com os pontos que estão corretos.\r\nc) Criar uma lista de 'naoConformidadesOuVerificar' com os pontos que estão incorretos ou não puderam ser verificados.\r\n\r\nFormate sua resposta estritamente como um objeto JSON seguindo o schema fornecido. Se uma informação não estiver visível, inclua-a na lista 'naoConformidadesOuVerificar'." } }
                },
                contents = new[]
                {
                new
                {
                    parts = new object[]
                    {
                        // A Imagem
                        new { inline_data = new { mime_type = mimeType, data = base64Image } },
                        // O Prompt com contexto do projeto
                        new { text = $"Analise esta imagem referente ao projeto descrito como: '{descricaoProjeto}'. Gere a saída estritamente seguindo o schema JSON fornecido." }
                    }
                }
            },
                generationConfig = new
                {
                    responseMimeType = "application/json",
                    responseSchema = schema, // aqui vai ser o esquema de resposta da ia, la em baixo a gente recebe a resposta e converte pra stringm
                    temperature = 0.2
                }
            };

            // a respostaaaaaaaaaaaaaaaaaaaaaaaaaa
            var jsonContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_baseUrl}?key={_apiKey}", jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                var errorMsg = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erro na API do Google: {errorMsg}");
            }

            //  extraindo a respostaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
            var jsonResponse = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(jsonResponse);

            // Navega no JSON de resposta do Google para pegar apenas o texto da análise
            try
            {
                string textoAnalise = doc.RootElement
                    .GetProperty("candidates")[0]
                    .GetProperty("content")
                    .GetProperty("parts")[0]
                    .GetProperty("text")
                    .GetString();

                return textoAnalise; // string em json limpa, tininda, impecável
            }
            catch
            {
                return "{}"; // Retorno de segurança
            }
        }

    }

}

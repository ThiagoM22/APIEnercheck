namespace APIEnercheck.Models.Gemini
{
    public class RespostaAnalise
    {
        public List<CategoriaAnalise> AnaliseCategorizada { get; set; }
    }
    public class CategoriaAnalise
    {
        public string Ctegoria { get; set; }
        public int PercentualConf { get; set; }
        public List<ItemAnalise> Conformidades { get; set; }
        public List<ItemAnalise> NaoConfVef { get; set; }
    }

    public class ItemAnalise

    {
        public string Item { get; set; }
        public string Observacao { get; set; }
    }
}

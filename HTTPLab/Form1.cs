using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using WinFormsApp1;

namespace HTTPLab
{
    public partial class Form1 : Form
    {
        List<CurrencyItem> items;
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpRequestMessage message = new HttpRequestMessage())
                {
                    message.Method = HttpMethod.Get;
                    message.RequestUri = new Uri(@"https://api.privatbank.ua/p24api/pubinfo?exchange&coursid=5");
                    HttpResponseMessage resp = await client.SendAsync(message);
                    string body = await resp.Content.ReadAsStringAsync();
                    textBox1.Text = body;
                     items = JsonSerializer.Deserialize<List<CurrencyItem>>(body);
                    foreach (var item in items)
                    {
                        comboBox1.Items.Add(item.Ccy);
                    }
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox2.Text = items[comboBox1.SelectedIndex].Description;
        }
    }
}